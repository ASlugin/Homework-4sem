module MiniCrawlerTests

open System
open NUnit.Framework
open Crawler
open FsUnit

[<Test>]
let Test1 () =
    let url = "https://se.math.spbu.ru/bachelor/admission.html"
    let expected = Seq.ofList [
          ("https://oops.math.spbu.ru/SE/alumni", Some(49175))
          ("https://www.acm.org/binaries/content/assets/education/curricula-recommendations/cc2005-march06final.pdf", Some(758040))
          ("https://guestbook.spbu.ru/2-uncategorised/8523-skolko-stoit-prozhivanie-v-obshchezhitii-dlya-studenta-na-byudzhete.html#:~:text=%D0%94%D0%BB%D1%8F%20%D0%BE%D0%B1%D1%83%D1%87%D0%B0%D1%8E%D1%89%D0%B8%D1%85%D1%81%D1%8F%20%D0%B7%D0%B0%20%D1%81%D1%87%D0%B5%D1%82%20%D0%B1%D1%8E%D0%B4%D0%B6%D0%B5%D1%82%D0%BD%D1%8B%D1%85,%D0%BF%D0%BB%D0%B0%D0%BD%D0%B8%D1%80%D0%BE%D0%B2%D0%BA%D0%B8%20%D0%B6%D0%B8%D0%BB%D1%8B%D1%85%20%D0%BF%D0%BE%D0%BC%D0%B5%D1%89%D0%B5%D0%BD%D0%B8%D0%B9%20%D0%B2%20%D0%BE%D0%B1%D1%89%D0%B5%D0%B6%D0%B8%D1%82%D0%B8%D0%B8.", Some(16904))
          ("https://students.spbu.ru/mmen-obwezhitija/5364-informatsiya-o-poselenii-pered-nachalom-uchebnogo-goda-2020.html", Some(43601))
          // ("https://github.com/embox/embox", Some(248197))
          ("https://guestbook.spbu.ru/2-uncategorised/8523-skolko-stoit-prozhivanie-v-obshchezhitii-dlya-studenta-na-byudzhete.html#:~:text=%D0%94%D0%BB%D1%8F%20%D0%BE%D0%B1%D1%83%D1%87%D0%B0%D1%8E%D1%89%D0%B8%D1%85%D1%81%D1%8F%20%D0%B7%D0%B0%20%D1%81%D1%87%D0%B5%D1%82%20%D0%B1%D1%8E%D0%B4%D0%B6%D0%B5%D1%82%D0%BD%D1%8B%D1%85,%D0%BF%D0%BB%D0%B0%D0%BD%D0%B8%D1%80%D0%BE%D0%B2%D0%BA%D0%B8%20%D0%B6%D0%B8%D0%BB%D1%8B%D1%85%20%D0%BF%D0%BE%D0%BC%D0%B5%D1%89%D0%B5%D0%BD%D0%B8%D0%B9%20%D0%B2%20%D0%BE%D0%B1%D1%89%D0%B5%D0%B6%D0%B8%D1%82%D0%B8%D0%B8.", Some(16904))
          ("https://students.spbu.ru/mmen-stipendii/stipendii/povyshennaya-akademicheskaya-stipendiya.html", Some(49012))
          ("https://oops.math.spbu.ru/SE/alumni", Some(49175)) 
        ]

    let result = getInfo url |> Async.RunSynchronously
    expected |> Seq.forall (fun x -> Seq.contains x result) |> should be True


[<Test>]
let ``getInfo should throw exception if link is incorrect`` () =
    (fun () ->
        getInfo "incorrect link" |> Async.RunSynchronously |> ignore)
    |> should throw typeof<InvalidOperationException>
    
[<Test>]
let Test3 () =
    let result = getInfo "https://www.youtube.com/" |> Async.RunSynchronously
    result |> should equal []