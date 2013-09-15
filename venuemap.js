var map;
function initialize() {

  var mapOptions = {
    zoom: 6,
    center: new google.maps.LatLng(51.4880407, -0.011816),
    mapTypeId: google.maps.MapTypeId.ROADMAP
  };
  map = new google.maps.Map(document.getElementById('map-canvas'),
      mapOptions);
    
          var latLng0 = new google.maps.LatLng(51.4880407, -0.011816);
          var string0 = '<h1>Our House</h1><p> London"';
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
        
}

google.maps.event.addDomListener(window, 'load', initialize);
