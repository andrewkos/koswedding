
#r @"C:\Users\Andrew\Documents\Visual Studio 2012\Projects\GoogleMapsJSGenerator\packages\FSharp.Data.1.1.10\lib\net40\FSharp.Data.dll"

type MapItem =
    { Name : string
      Address : string
      Description : string
      LatLng : (decimal*decimal) option
      Colour : string }

type GeocodeResponse = FSharp.Data.JsonProvider<"""{
       "results" : [
          {
             "address_components" : [
                {
                   "long_name" : "1600",
                   "short_name" : "1600",
                   "types" : [ "street_number" ]
                },
                {
                   "long_name" : "Amphitheatre Pkwy",
                   "short_name" : "Amphitheatre Pkwy",
                   "types" : [ "route" ]
                },
                {
                   "long_name" : "Mountain View",
                   "short_name" : "Mountain View",
                   "types" : [ "locality", "political" ]
                },
                {
                   "long_name" : "Santa Clara",
                   "short_name" : "Santa Clara",
                   "types" : [ "administrative_area_level_2", "political" ]
                },
                {
                   "long_name" : "California",
                   "short_name" : "CA",
                   "types" : [ "administrative_area_level_1", "political" ]
                },
                {
                   "long_name" : "United States",
                   "short_name" : "US",
                   "types" : [ "country", "political" ]
                },
                {
                   "long_name" : "94043",
                   "short_name" : "94043",
                   "types" : [ "postal_code" ]
                }
             ],
             "formatted_address" : "1600 Amphitheatre Pkwy, Mountain View, CA 94043, USA",
             "geometry" : {
                "location" : {
                   "lat" : 37.42291810,
                   "lng" : -122.08542120
                },
                "location_type" : "ROOFTOP",
                "viewport" : {
                   "northeast" : {
                      "lat" : 37.42426708029149,
                      "lng" : -122.0840722197085
                   },
                   "southwest" : {
                      "lat" : 37.42156911970850,
                      "lng" : -122.0867701802915
                   }
                }
             },
             "types" : [ "street_address" ]
          }
       ],
       "status" : "OK"
    }""">

let latLng (address:string) =
    let fetch = sprintf @"http://maps.googleapis.com/maps/api/geocode/json?address=%s&sensor=false" (address.Replace(" ","+") + ",NY")
    use resp = System.Net.WebRequest.Create(fetch).GetResponse().GetResponseStream()
    System.Threading.Thread.Sleep(100)
    use reader = new System.IO.StreamReader(resp)
    let result = GeocodeResponse.Parse(reader.ReadToEnd())
    try Some(result.Results.[0].Geometry.Location.Lat, result.Results.[0].Geometry.Location.Lng) with | _ -> None


let item1 =
    { Name = "Apotheke"
      Address = "9 Doyers St"
      Description = "Speakeasy. requires password from @apothekenyc Twitter on Wednesdays."
      LatLng = Some(0M,0M)
      Colour = "Red"}

let projectPath = @"C:\Users\Andrew\Documents\GitHub\koswedding\"

let dodgyChar = char 65533

let file = System.IO.File.OpenText(projectPath + "venues.csv").ReadToEnd().Trim().Replace(dodgyChar,' ').Replace("'","")

let itemsArray = file.Split([|'\n'|]) |> Array.map (fun i -> let arr = i.Trim().Split([|','|])
                                                             { Name = arr.[0];
                                                               Address = arr.[1];
                                                               Description = arr.[2]
                                                               LatLng = latLng (arr.[1])
                                                               Colour = "red"})


let jsStart = "var map;
function initialize() {

  var mapOptions = {
    zoom: 13,
    center: new google.maps.LatLng(40.722283, -73.98747),
    mapTypeId: google.maps.MapTypeId.ROADMAP
  };
  map = new google.maps.Map(document.getElementById('map-canvas'),
      mapOptions);
"
let jsEnd = "
}

google.maps.event.addDomListener(window, 'load', initialize);
"

let jsItem i (name:string) lat lng description colour =
    let newName = name.Replace(" ","_")
    sprintf
        "    
          var latLng%i = new google.maps.LatLng(%M, %M);
          var string%i = '<h1>%s</h1><p>%s';
          var window%i = new google.maps.InfoWindow({
              content: string%i
          });
          var marker%i = new google.maps.Marker({
              position: latLng%i,
              map: map,
              title: '%s',
              icon: 'http://maps.google.com/mapfiles/ms/icons/%s-dot.png'
          });
          google.maps.event.addListener(marker%i, 'mouseover', function () {
              window%i.open(map, marker%i);
          });
          google.maps.event.addListener(marker%i, 'mouseout', function () {
              window%i.close();
          });
        " i lat lng i name description i i i i name colour i i i i i

let resultString =
    let items = itemsArray
                |> Array.filter (fun i -> i.LatLng.IsSome)
                |> Array.mapi (fun i v -> jsItem i (v.Name) (fst v.LatLng.Value) (snd v.LatLng.Value) (v.Description) (v.Colour))
    printfn "%A" items.Length
    jsStart + (String.concat "" items) + jsEnd

let testResultString =
    let items = itemsArray
                |> Array.filter (fun i -> i.LatLng.IsSome)
                |> fun arr -> arr.[0..0]
                |> Array.mapi (fun i v -> jsItem i (v.Name) (fst v.LatLng.Value) (snd v.LatLng.Value) (v.Description) (v.Colour))
    printfn "%A" items.Length
    jsStart + (String.concat "" items) + jsEnd    

let outputFile = System.IO.File.CreateText(projectPath + "venuemap.js")
outputFile.Write(resultString)
outputFile.Close()

itemsArray |> Array.filter (fun i -> i.LatLng.Value <> (0M,0M))



