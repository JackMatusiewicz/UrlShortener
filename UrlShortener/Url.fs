module Url

open Suave
open Suave.Successful
open System.Security.Cryptography
open System.Text
open System.Collections.Generic

let createHashFunction () =
    let sha256 = SHA256Managed.Create()
    let getHash (hash : SHA256) (url : string) : byte[] =
       hash.ComputeHash(Encoding.Default.GetBytes(url)).[..4]
    sha256 |> getHash

let shorten url = createHashFunction () url |> Base62.toBase62