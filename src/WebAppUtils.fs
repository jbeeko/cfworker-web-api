module WebAppUtils
open WorkersInterop
open Fetch

type Verb =
    | GET
    | POST
    | PUT
    | PATCH
    | DELETE
    | OPTION
    | HEAD
    | TRACE
    | CONNECT
    | UNDEFINED

let verb (r:Request) =
    match r.method.ToUpper() with
    | "GET" -> GET
    | "POST" -> POST
    | "PUT" -> PUT
    | "PATCH" -> PATCH
    | "DELETE" -> DELETE
    | "OPTION" -> OPTION
    | "HEAD" -> HEAD
    | "TRACE" -> TRACE
    | "CONNECT" -> CONNECT
    | _ -> UNDEFINED

let noHandler (req:CFWRequest) =
  newResponse (sprintf "No handler for: %s on: %s" req.method req.url ) "404" |> wrap
