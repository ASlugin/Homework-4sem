module LocalNetworkTest

open NUnit.Framework
open LocalNetwork

[<Test>]
let TestNetworkWhenAllComputersAreClear () =
    let computers = [Computer(Linux, "B", Clear); Computer(Windows, "A", Clear); Computer(MacOS, "C", Clear); Computer(MacOS, "D", Clear)]
    let connections = [("A", "B"); ("A", "C"); ("B", "D")]
    
    let network = Network(computers, connections, (fun () -> 0.05))
    network.Run()
    
    Assert.IsTrue(computers.Equals(network.getComputers))

[<Test>]
let TestNetworkWhenAllComputersAreInfected () =
    let computers = [Computer(Linux, "B", Infected); Computer(Windows, "A", Infected); Computer(MacOS, "C", Infected); Computer(MacOS, "D", Infected)]
    let connections = [("A", "B"); ("A", "C"); ("B", "D")]
    
    let network = Network(computers, connections, (fun () -> 0.1))
    network.Run()
    
    Assert.IsTrue(computers.Equals(network.getComputers))

[<Test>]
let TestNetworkWhenSomeComputersAreInfected () =
    let computers = [Computer(Linux, "A", Clear); Computer(Windows, "B", Infected); Computer(MacOS, "C", Clear)]
    let connections = [("B", "A"); ("B", "C")]
    let expected = [Computer(Linux, "A", Clear); Computer(Windows, "B", Infected); Computer(MacOS, "C", Infected)]
    
    let network = Network(computers, connections, (fun () -> 0.6))
    network.Run()
    
    Assert.IsTrue(expected.Equals(network.getComputers))
