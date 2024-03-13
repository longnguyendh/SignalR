var reactjsSpan = document.getElementById("reactjsCounter");
var javaSpan = document.getElementById("javaCounter");
var csharpSpan = document.getElementById("csharpCounter");


//create connection
var connectionDeathlyHallows = new signalR.HubConnectionBuilder()
    //.configureLogging(signalR.LogLevel.Information)
    .withUrl("/hubs/viewerCounter").build();

//connect to methods that hub invokes aka receive notfications from hub
connectionDeathlyHallows.on("updateCourseCount", (reactjs, java, csharp) => {
    reactjsSpan.innerText = reactjs.toString();
    javaSpan.innerText = java.toString();
    csharpSpan.innerText = csharp.toString();
});



//invoke hub methods aka send notification to hub

//start connection
function fulfilled() {

    connectionDeathlyHallows.invoke("GetRaceStatus").then((raceCounter) => {
        reactjsSpan.innerText = raceCounter.Reactjs.toString();
        javaSpan.innerText = raceCounter.Java.toString();
        csharpSpan.innerText = raceCounter.Csharp.toString();
    });
    //do something on start
    console.log("Connection to User Hub Successful");
}
function rejected() {
    //rejected logs
}

connectionDeathlyHallows.start().then(fulfilled, rejected);