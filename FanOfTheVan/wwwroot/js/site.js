function getUserDefaultLocation() {
    return { latitude: 51.514007, longitude: -0.098647 };
}

function initialiseMap() {
    var startingLocation = getUserDefaultLocation();
    return new Microsoft.Maps.Map(document.getElementById('myMap'), {
        center: new Microsoft.Maps.Location(startingLocation.latitude, startingLocation.longitude),
        zoom: 15,
        showLocateMeButton: false,
        showMapTypeSelector: false
    });
}

function attachClickHandlers(polygons) {
    for (var i = 0; i < polygons.length; i++) {
        Microsoft.Maps.Events.addHandler(polygons[i], 'click', function (e) {
            showmarketDetails(e.target.entity.id, e.location);
        });
    }
}

function handleMarketPointClickEvent(e, map) {
    var marketId = e.target.Metadata.Id;
    $(".market.selected").removeClass("selected");
    var $market = $(".market[data-id=" + marketId + "]");
    $market.addClass("selected");
    var $marketsDiv = $("#marketsDiv");
    $marketsDiv.animate({ scrollTop: $market.position().top });
}

function handleMarketDescriptionClickEvent(marketPin, map) {
    var marketId = marketPin.Metadata.Id;
    var $market = $(".market[data-id=" + marketId + "]");

    $market.click(function () {
        centrePin(marketPin, map);
        $(".market.selected").removeClass("selected");
        $market.addClass("selected");
    })
}

function cleanPostcodeInput(postcode) {
    return postcode.replace(" ", "");
}

function getLatLongUrl(postcode) {
    return "https://dev.virtualearth.net/REST/v1/Locations?countryRegion=UK&postalCode=" + postcode + "&key=AhBX1hdku8N4JQgp-V_aA5mMDhwK9Ve4RMN0dKU1_ubY9KkKvlZpSnO8ofe3dS_X";
}

function getLatLong(postcode) {
    var url = getLatLongUrl(postcode);
    return $.get(url);
}

function getSearchTerm() {
    var searchTerm = $("#searchText").val();
    return cleanPostcodeInput(searchTerm);
}

function setSearchTerm(value) {
    $("#searchText").val(value);
}

function getSelectedDistance() {
    var distance = $("#distanceSelect").val();
    return parseInt(distance);
}

function setSelectedDistance(value) {
    $("#distanceSelect").val(value);
}

function getOpenStatus() {
    var openStatus = $("#openStatusSelect").val();
    return parseInt(openStatus);
}

function setOpenStatus(value) {
    $("#openStatusSelect").val(value);
}

function centrePin(locationPin, map) {
    map.setView({
        mapTypeId: Microsoft.Maps.MapTypeId.canvasLight,
        center: new Microsoft.Maps.Location(parseFloat(locationPin.Metadata.Lat), parseFloat(locationPin.Metadata.Long))
    });
}

function addUserPosition(lat, long, map, locationPin) {
    if (locationPin) {
        map.entities.remove(locationPin);
    }
    var locationPoint = new Microsoft.Maps.Location(lat, long);
    locationPin = new Microsoft.Maps.Pushpin(locationPoint,
        {
            title: "You"
        });
    map.entities.push(locationPin);
    return locationPin;
}

function populateMarketDetails(marketsHtml, $marketsDiv) {
    $marketsDiv.html(marketsHtml);
}

function drawMarketsOnMap(map, userLat, userLong) {
    var $markets = $(".market");
    var pinLayer = new Microsoft.Maps.EntityCollection();
    var boundingBox = { minLat: userLat, maxLat: userLat, minLong: userLong, maxLong: userLong };

    for (var i = 0; i < $markets.length; i++) {
        var market = $markets[i];
        var id = market.getAttribute("data-id");
        var lat = parseFloat(market.getAttribute("data-lat"));
        var long = parseFloat(market.getAttribute("data-long"));

        if (boundingBox.minLat > lat) {
            boundingBox.minLat = lat;
        }
        else if (boundingBox.maxLat < lat) {
            boundingBox.maxLat = lat;
        }

        if (boundingBox.minLong > long) {
            boundingBox.minLong = long;
        }
        else if (boundingBox.maxLong < long) {
            boundingBox.maxLong = long;
        }

        var name = market.getAttribute("data-name");
        var colour = market.getAttribute("data-colour");
        var pinColour = Microsoft.Maps.Color.fromHex(colour);
        var point = new Microsoft.Maps.Location(lat, long);
        var pin = new Microsoft.Maps.Pushpin(point,
            {
                color: pinColour,
                title: name,
                enableHoverStyle: true
            });

        pin.Metadata = { Id: id, Lat: lat, Long: long };
        pinLayer.push(pin);

        Microsoft.Maps.Events.addHandler(pin, 'click', function (e) {
            handleMarketPointClickEvent(e, map);
        });
        handleMarketDescriptionClickEvent(pin, map);
    }
    map.entities.push(pinLayer);
    updateMapView(map, boundingBox);
}

function updateMapView(map, boundingBox) {
    var latPadding = (boundingBox.maxLat - boundingBox.minLat) / 20;
    var longPadding = (boundingBox.maxLong - boundingBox.minLong) / 20;

    map.setView({
        mapTypeId: Microsoft.Maps.MapTypeId.canvasLight,
        bounds: Microsoft.Maps.LocationRect.fromEdges(boundingBox.maxLat + latPadding,
            boundingBox.minLong - longPadding,
            boundingBox.minLat - latPadding,
            boundingBox.maxLong + longPadding)
    });
}

function toggleShowHours() {
    var $this = $(this);
    $this.addClass("open").removeClass("closed");
    $this.find(".hours-arrow-icon").removeClass("glyphicon-triangle-bottom").addClass("glyphicon-triangle-top");
    $this.find(".hours-div").removeClass("hidden");
}

function toggleHideHours() {
    var $this = $(this);
    $this.addClass("closed").removeClass("open");
    $this.find(".hours-arrow-icon").removeClass("glyphicon-triangle-top").addClass("glyphicon-triangle-bottom");
    $this.find(".hours-div").addClass("hidden");
}

function toggleShowDescription() {
    var $descriptionBlurb = $(this);
    $descriptionBlurb.addClass("hidden").next(".description").removeClass("hidden");
}

function saveSearchTerms(lat, long, distance, openStatus, searchTerm) {
    document.cookie = "search=" + lat + "|" + long + "|" + distance + "|" + openStatus + "|" + searchTerm;
}

function getSearchTermsFromCookie() {
    if (!document.cookie) {
        return;
    }

    var cookies = document.cookie.split("; ");
    var cookie;
    for (var i = 0; i < cookies.length; i++) {
        var nameValue = cookies[i].split("=");
        var name = nameValue[0];
        if (name === "search") {
            cookie = nameValue[1];
        }
    }

    if (!cookie) {
        return;
    }

    var terms = cookie.split("|");
    return { lat: parseFloat(terms[0]), long: parseFloat(terms[1]), distance: parseInt(terms[2]), openStatus: terms[3], searchTerm: terms[4] };
}

function loadMapScenario() {
    var map = initialiseMap();
    var locationPin;

    $(function () {
        var $marketsDiv = $("#marketsDiv");
        var $resultsContainer = $("#results-div");

        $marketsDiv.on("click", ".hours-toggle.closed", toggleShowHours);
        $marketsDiv.on("click", ".hours-toggle.open", toggleHideHours);
        $marketsDiv.on("click", ".description-blurb", toggleShowDescription);

        $("#searchBtn").click(function () {
            var searchTerm = getSearchTerm();
            if (!searchTerm) {
                return;
            }
            map.entities.clear();
            getLatLong(searchTerm).done(handleSearchPositionResult);
        });

        function populateSearchFromCookie() {
            var searchCookie = getSearchTermsFromCookie();
            if (!searchCookie) {
                return;
            }

            setSelectedDistance(searchCookie.distance);
            setSearchTerm(searchCookie.searchTerm);
            setOpenStatus(searchCookie.openStatus);
            locationPin = addUserPosition(searchCookie.lat, searchCookie.long, map, locationPin);
            drawMarketsOnMap(map, searchCookie.lat, searchCookie.long);
            $resultsContainer.removeClass("invisible");
        }

        function handleSearchPositionResult(data) {
            if (!data.resourceSets || !data.resourceSets.length || !data.resourceSets[0].resources.length || !data.resourceSets[0].resources[0].geocodePoints.length) {
                return;
            }
            var latlong = data.resourceSets[0].resources[0].geocodePoints[0].coordinates;
            var distance = getSelectedDistance();
            var openStatus = getOpenStatus();
            updateResults(latlong[0], latlong[1], distance, openStatus)
        }

        function updateResults(lat, long, distance, openStatus) {
            locationPin = addUserPosition(lat, long, map, locationPin);
            $resultsContainer.removeClass("invisible");
            var url = $(".filters").data("url");
            $.post(url, { lat: lat, longi: long, distance: distance, openStatus: openStatus }).done(function (html) {
                populateMarketDetails(html, $marketsDiv);
                drawMarketsOnMap(map, lat, long);
            });
            saveSearchTerms(lat, long, distance, openStatus, getSearchTerm());
        }

        populateSearchFromCookie();
    });
}
