module WorkersInterop

open System.Text.RegularExpressions

open Fable.Core
open Fable.Core.JS
open Browser.Types
open Fetch


type [<AllowNullLiteral>] FetchEvent =
  inherit Event
  abstract request: Request with get, set
  abstract respondWith: response: U2<Promise<Response>, Response> -> Promise<Response>

[<Emit("addEventListener('fetch', $0)")>]
let addEventListener (e:FetchEvent->Promise<Response>) : unit = jsNative

[<Emit("new Response($0, {status: $1})")>]
let newResponse (a:string) (b:string) : Response = jsNative

type CFWRequest =
    inherit Request
    abstract member cf : CFDetails
and CFDetails = {
    tlsVersion: string
    tlsCipher: string
    country: string
    colo: string
}

let wrap x = promise {return x}

let path (r:CFWRequest)=
    match Regex.Split((System.Uri r.url).AbsolutePath.ToLower(), "\/") with
    | [|"";""|] -> [||]   // this is for paths http://somthing.com/ and http://something.com
    | p -> p.[1..]        // becuase the first path element is always ""
    |> List.ofArray

let textResponse txt =
  newResponse txt "200" |> wrap


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

let verb (r:CFWRequest) =
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
