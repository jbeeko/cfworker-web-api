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
    asn: string
    colo: string
    weight: string
    tlsVersion: string
    tlsCipher: string
    country: string
}

let wrap x = promise {return x}

let path (r:CFWRequest)=
    Regex.Split((System.Uri r.url).AbsolutePath.ToLower(), "\/")
    |> List.ofArray
    |> List.filter (fun s -> s.Length <> 0)

let textResponse txt =
  newResponse txt "200" |> wrap

