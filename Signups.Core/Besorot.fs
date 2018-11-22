namespace Signups.Core
open FSharp.Data
open System

module Besorot =
    type BesorahReadings = JsonProvider<"./besorah-data.json">
    //type Reading = {
    //    parasha: string;
    //    reading: string
    //}

    type Year = A | B | C

    type ReadingRec = { Parasha: string; Reading: string }

    let CycleA = [
        {Parasha = "Parashat Bereshit"; Reading = "John 1:1–18"}
        ]

    let CycleB = [
        {Parasha = "Parashat Bereshit"; Reading = "John 1:1–18"}
        ]

    let CycleC = [
        {Parasha = "Parashat Bereshit"; Reading = "John 1:1–18"}
        ]

    let BesorotData (year: Year) =
        match year with
        | A -> CycleA
        | B -> CycleB
        | C -> CycleC
        

    //type Readings =
    //    | year of Year
    //    | reading of list Reading

    let GetYear (calendarYear: int) = 
        match (calendarYear - 5771) % 3 with
        | 0 -> Year.A
        | 1 -> Year.B
        | _ -> Year.C

    let GetReading calendarYear parasha =
        let year = GetYear calendarYear
        let readings = BesorotData year

        let found = readings |> Seq.find(fun i -> i.Parasha = parasha)
        found

        


