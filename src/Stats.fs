module Stats
open WorkersInterop
open System

type private StateRecord ={
  ave : float
  sum : float
  count : int
}
let mutable private state =  {ave = nan; sum = 0.0; count = 0}
let private rand = Random 1

// Return a timestame, a count of the number if invokations seen by the running worker as well as some
// information from the CloudFlare specific part of th request as a Response promise
let stats (req: CFWRequest) =
    let num = rand.NextDouble() * 10.0
    let sum, count = (state.sum + num, state.count + 1)
    let cf = req.cf
    state <- {ave  = sum/(float count); sum = sum + num; count = count}
    let resp =
      newResponse
        (sprintf "Some request stats:\n\nColo: %s Cntry: %s\nTLSVersion: %s TLSCiper: %s\nCount: %i Ave %f\nTime: %O"  cf.colo cf.country cf.tlsVersion cf.tlsCipher state.count state.ave DateTime.Now)
        "200"
    resp |> wrap
