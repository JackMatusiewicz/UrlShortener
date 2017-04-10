open Suave
open Suave.Web
open Suave.Http
open Suave.Filters
open Suave.Operators
open Suave.Successful
open Suave.Cookie
open System.Collections.Generic

let redirectTo (store : Dictionary<string, string>) (url : string) : WebPart =
    match store.ContainsKey(url) with
    | false -> OK (sprintf "Unable to find url for sequence: %s" url)
    | true -> Redirection.redirect store.[url]  

let returnShortenedUrl (store : Dictionary<string,string>) (url : string) : WebPart =
    let shortenedUrl = Url.shorten url
    if store.ContainsKey(shortenedUrl) = false then  
        store.Add(shortenedUrl, url)
    OK (sprintf "localhost:8080/%s" shortenedUrl)

[<EntryPoint>]
let main argv = 
    let store = Dictionary<string, string>();

    let shorten = returnShortenedUrl store
    let redirect = redirectTo store

    let app = choose [
                pathScan "/shorten/%s" shorten
                pathScan "/%s" redirect
            ]
    startWebServer defaultConfig app
    0