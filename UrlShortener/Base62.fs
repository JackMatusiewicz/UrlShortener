﻿module Base62
open System
open System.Text

let base36Characters = ['a' .. 'z'] @ ['A'..'Z'] @ ['0' .. '9']
let numberOfCharacters : byte = (byte base36Characters.Length)

let convertCharToBase62 (data : byte) : (char list) = 
    let rec convert (data : byte) (acc : char list) : (char list) =
        match data with
        | _ when data < numberOfCharacters -> (base36Characters.[(int data)]) :: acc
        | _ -> convert (data / numberOfCharacters) (base36Characters.[((int data) % (int numberOfCharacters))] :: acc)
    convert data []

let toBase62 (data : byte[]) : string =
    let rec convert (data : byte[] ) (index : int) (acc : char list) =
        match index with
        | _ when index >= data.Length -> acc
        | _ -> convert (data) (index + 1) ((convertCharToBase62 data.[index]) @ acc)
    let chars = convert data 0 []
    string (List.fold (fun (sb:StringBuilder) (c:char) -> sb.Append(c)) 
                      (new StringBuilder()) chars)
