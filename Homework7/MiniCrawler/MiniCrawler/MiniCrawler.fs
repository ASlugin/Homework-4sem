module Crawler

open System.Net.Http
open System.Text.RegularExpressions

let fetchLinks (html: string) =
    let pattern = "<a href=\"(https?:\/\/\S+\W*)\"[^>]*>"
    let regex = Regex(pattern, RegexOptions.Compiled)
    let matches = regex.Matches(html)
    matches |> Seq.map (fun item -> item.Groups[1].Value)

let downloadPage (url: string) (client: HttpClient) =
    client.GetStringAsync(url) |> Async.AwaitTask |> Async.Catch

let getSizes pages =
    pages
    |> Seq.map (fun page ->
        match page with
        | Choice1Of2 (x: string) -> Some x.Length
        | Choice2Of2 (_: exn) -> None
        )

let getInfo (url: string) =
    async {
        let client = new HttpClient()
        let! page = downloadPage url client
        let html = 
            match page with
            | Choice1Of2 result -> Some result
            | Choice2Of2 (_: exn) -> None

        if html = None then
            return Seq.empty
        else 
            let links = fetchLinks html.Value
            let! pages =
                links
                |> Seq.map (fun link -> downloadPage link client)
                |> Async.Parallel
            return getSizes pages |> Seq.zip links
    }