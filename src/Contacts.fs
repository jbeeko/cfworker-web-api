module Contacts

open Thoth.Json

open WorkersInterop
open WebAppUtils

type Contact = {
    id: string
    FirstName: string
    FamillyName: string
    DOB: string
    EMail: string
} with
    member x.Name = System.String.Join(" ", [|x.FirstName.Trim(); x.FamillyName.Trim()|])
let  contactDecoder = Decode.Auto.generateDecoder<Contact>()


// Contact sub-WebApp. Just like the primary WebApp it gets the route path elements
// passed to it and matches on parts.
let rec routeRequest (verb: Verb) (path: string list) (req: CFWRequest) =
    match (verb, path) with
    | GET, [i] ->   if i.EndsWith("~")
                    then getContacts req (Some (i.Substring(0, i.Length - 1)))
                    else getContact req i
    | GET, [] ->    getContacts req None
    | POST, [] ->   postContact req
    | PUT, [i] ->   putContact req i
    | DELETE, [i] ->   deleteContact req i
    | _, _ ->       noHandler req

and private getContacts req prefix  =
    promise {
        let! resp = KVStore.keys prefix
        let! cntcts =
            resp.keys
            |> Array.map (fun k -> KVStore.get k.name)
            |> Promise.Parallel
        let json =
            cntcts
            |> Array.filter (fun c -> c.IsSome)
        return newResponse (json.ToString()) "200"
    }

and private getContact req i =
    if i.EndsWith("~")
    then
        let prefix = Some (i.Substring(0, i.Length - 1))
        getContacts req prefix
    else
        promise {
            match! KVStore.get i with
            | None -> return newResponse (sprintf "No contact with id %s" i) "404"
            | Some json -> return newResponse json "200"
        }

and private postContact req  =
    promise {
         let! body = (req.text())
         let contact = Decode.fromString contactDecoder body
         match contact with
         | Ok c ->
            match! KVStore.get c.id with
            | Some _ ->
                return newResponse ("Id exists: "+ c.id ) "409"
            | None ->
                do! KVStore.put c.id body
                return newResponse body "200"
         | Error e -> return newResponse (sprintf "Unable to process: %s because: %O" body e) "200"
    }

and private putContact req i =
    promise {
        let! body = (req.text())
        let updatedContact = Decode.fromString contactDecoder body
        match! KVStore.get i with
        | Some d ->
            let existingContact = Decode.fromString contactDecoder d
            match existingContact, updatedContact with
            | Ok existing, Ok updated ->
               if existing.id = i
               then
                   do! KVStore.put i body
                   return newResponse body "200"
               else return newResponse (sprintf "Unable to process put.") "400"
            | _,_ -> return newResponse (sprintf "Unable to process put.") "400"
        | None -> return newResponse (sprintf "Contact not found %s" i) "404"
    }

and private deleteContact req i  =
    promise {
        match! KVStore.get i with
        | None -> return newResponse (sprintf "No contact with id %s" i) "404"
        | Some json ->
            do! KVStore.delete i
            return newResponse json"200"
    }

and private noHandler req  =
    newResponse "Invalid contact request" "400" |> wrap
