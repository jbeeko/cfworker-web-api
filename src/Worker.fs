module Worker

open Fable.Core
open WorkersInterop
open WebAppUtils

// Dispatch to the web App wich will generate a
// Response and return it as a Promise<Response>
let private handleRequest (req:CFWRequest) =
    WebApp.routeRequest (verb req) (path req) req

// Register a listner for the ServiceWorker 'fetch' event
addEventListener (fun (e:FetchEvent) ->
  e.respondWith (U2.Case1 (handleRequest (e.request :?> CFWRequest))))

