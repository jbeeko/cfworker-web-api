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

type KeyQueryResponse1 = {
    keys: Key list
    list_complete: bool
    cursor: string option
}

type ResultInfo = {
    count: int
    cursor: string option
}


type KeyQueryResponse = {
    result: Key list
    success: bool
    errors: string list
    messages: string list
    result_info: ResultInfo
}

[<Emit("KVStore.get($0)")>]
let get(key:string) : JS.Promise<string option> = jsNative

[<Emit("KVStore.put($0,$1)")>]
let put(key:string) (value:string) : JS.Promise<unit> = jsNative

[<Emit("KVStore.delete($0)")>]
let delete(key:string) : JS.Promise<unit> = jsNative


