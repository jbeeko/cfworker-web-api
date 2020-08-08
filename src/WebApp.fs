module WebApp

open WorkersInterop
open WebAppUtils

// WebApp router written as a match statement on the HTTP Verb and the route.
// It handles multi-part routes, route variables, and subroutes. Inspired by
// the ReasonML router.
let rec routeRequest verb path req =
  match (verb, path) with
  | GET, [] ->                  textResponse "Home sweet home!!"
  | GET, ["hello"] ->           textResponse (sprintf "Hello world from F# at: %A" System.DateTime.Now)
  | GET, ["bye"] ->             textResponse "Goodbye cruel world."
  | GET, ["hello"; "bye"] ->    textResponse "I say hello and the goodbye."
  | GET, ["hello"; "bye"; i] -> textResponse (sprintf "Hello-bye id: %s" i)
  | GET, ["stats"] ->           Stats.stats req
  | _, "contacts"::subPath ->   Contacts.routeRequest verb subPath req
  | _,_ ->                      noHandler req
