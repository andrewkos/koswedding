var map;
function initialize() {

  var mapOptions = {
    zoom: 9,
    center: new google.maps.LatLng(51.488, -0.011),
    mapTypeId: google.maps.MapTypeId.ROADMAP
  };
  map = new google.maps.Map(document.getElementById('map-canvas'),
      mapOptions);
    
          var latLng0 = new google.maps.LatLng(51.4880407, -0.011816);
          var string0 = '<h1>Our House</h1><p>Our House';
          var window0 = new google.maps.InfoWindow({
              content: string0
          });
          var marker0 = new google.maps.Marker({
              position: latLng0,
              map: map,
              title: 'Our House',
              icon: 'http://maps.google.com/mapfiles/ms/icons/red-dot.png'
          });
          google.maps.event.addListener(marker0, 'mouseover', function () {
              window0.open(map, marker0);
          });
          google.maps.event.addListener(marker0, 'mouseout', function () {
              window0.close();
          });
            
          var latLng1 = new google.maps.LatLng(51.5606332, -0.08866309999999999);
          var string1 = '<h1>Clissold House</h1><p>A touch small?';
          var window1 = new google.maps.InfoWindow({
              content: string1
          });
          var marker1 = new google.maps.Marker({
              position: latLng1,
              map: map,
              title: 'Clissold House',
              icon: 'http://maps.google.com/mapfiles/ms/icons/red-dot.png'
          });
          google.maps.event.addListener(marker1, 'mouseover', function () {
              window1.open(map, marker1);
          });
          google.maps.event.addListener(marker1, 'mouseout', function () {
              window1.close();
          });
            
          var latLng2 = new google.maps.LatLng(51.560722, -0.083483);
          var string2 = '<h1>Stoke Newington Town Hall</h1><p>Art Deco Townhall';
          var window2 = new google.maps.InfoWindow({
              content: string2
          });
          var marker2 = new google.maps.Marker({
              position: latLng2,
              map: map,
              title: 'Stoke Newington Town Hall',
              icon: 'http://maps.google.com/mapfiles/ms/icons/red-dot.png'
          });
          google.maps.event.addListener(marker2, 'mouseover', function () {
              window2.open(map, marker2);
          });
          google.maps.event.addListener(marker2, 'mouseout', function () {
              window2.close();
          });
            
          var latLng3 = new google.maps.LatLng(51.466908, -0.2161665);
          var string3 = '<h1>Star and Garter</h1><p>Nice Pub';
          var window3 = new google.maps.InfoWindow({
              content: string3
          });
          var marker3 = new google.maps.Marker({
              position: latLng3,
              map: map,
              title: 'Star and Garter',
              icon: 'http://maps.google.com/mapfiles/ms/icons/red-dot.png'
          });
          google.maps.event.addListener(marker3, 'mouseover', function () {
              window3.open(map, marker3);
          });
          google.maps.event.addListener(marker3, 'mouseout', function () {
              window3.close();
          });
            
          var latLng4 = new google.maps.LatLng(51.5060699, -0.0275537);
          var string4 = '<h1>Battery</h1><p>Entertaining';
          var window4 = new google.maps.InfoWindow({
              content: string4
          });
          var marker4 = new google.maps.Marker({
              position: latLng4,
              map: map,
              title: 'Battery',
              icon: 'http://maps.google.com/mapfiles/ms/icons/red-dot.png'
          });
          google.maps.event.addListener(marker4, 'mouseover', function () {
              window4.open(map, marker4);
          });
          google.maps.event.addListener(marker4, 'mouseout', function () {
              window4.close();
          });
        
}

google.maps.event.addDomListener(window, 'load', initialize);
