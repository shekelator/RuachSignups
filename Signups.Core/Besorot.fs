namespace Signups.Core
open FSharp.Data
open System

module Besorot =
    type BesorahReadings = JsonProvider<"./besorah-data.json">
    //type Reading = {
    //    parasha: string;
    //    reading: string
    //}

    //type Year = A | B | C

    //type Readings =
    //    | year of Year
    //    | reading of list Reading


    let GetReading data year parasha =
        let readings = (BesorahReadings.Parse data).A
        let found = readings |> Seq.find(fun i -> i.Parasha = parasha)
        found

        


