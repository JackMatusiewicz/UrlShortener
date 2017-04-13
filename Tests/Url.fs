module Test.Url

open NUnit.Framework
open Url

[<Test>]
let ``Given a url when I shorten it twice then result is the same`` () =
    let firstHash = Url.shorten "www.facebook.com"
    let secondHash = Url.shorten "www.facebook.com"
    Assert.That(firstHash, Is.EqualTo(secondHash))

[<Test>]
let ``Given two different urls when hashed then they are different`` () =
    let firstHash = Url.shorten "www.google.com"
    let secondHash = Url.shorten "www.facebook.com"
    Assert.That(firstHash, Is.Not.EqualTo(secondHash))


