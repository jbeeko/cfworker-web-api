module KVStore

open Fable.Core

type KeyQuery = {
    prefix: string
    cursor: string option
}

type Key = {
    name: string
    expiration: int option
}

type KeyQueryResponse = {
    keys: Key []
    list_complete: bool
    cursor: string option
}

type ResultInfo = {
    count: int
    cursor: string option
}

[<Emit("KVStore.list($0)")>]
let keyQuery(prefix) : JS.Promise<KeyQueryResponse> = jsNative

let keys (prefix:string option) =
    keyQuery {|prefix = prefix|}
    
[<Emit("KVStore.get($0)")>]
let get(key:string) : JS.Promise<string option> = jsNative

[<Emit("KVStore.put($0,$1)")>]
let put(key:string) (value:string) : JS.Promise<unit> = jsNative

[<Emit("KVStore.delete($0)")>]
let delete(key:string) : JS.Promise<unit> = jsNative


