const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

connection.on("ReceiveMessage", (user, message) => {
    const messagesList = document.getElementById("messagesList");
    const listItem = document.createElement("li");

    // Create a div with appropriate class based on user or system message
    const messageDiv = document.createElement("div");
    messageDiv.textContent = `${user}: ${message}`;

    // Apply the message class
    messageDiv.classList.add(user === "System" ? "system-message" : "message");

    listItem.appendChild(messageDiv);
    messagesList.appendChild(listItem);
});

connection.start().catch(err => console.error(err));

function joinGroup() {
    const groupName = document.getElementById("groupInput").value;
    const user = document.getElementById("userInput").value;
    connection.invoke("JoinGroup", user, groupName).catch(err => console.error(err));
}

function leaveGroup() {
    const groupName = document.getElementById("groupInput").value;
    const user = document.getElementById("userInput").value;
    connection.invoke("LeaveGroup", user, groupName).catch(err => console.error(err));
}

function sendMessage() {
    const user = document.getElementById("userInput").value;
    const message = document.getElementById("messageInput").value;
    const groupName = document.getElementById("groupInput").value;
    connection.invoke("SendMessage", user, message, groupName).catch(err => console.error(err));
}

function clearMessages() {
    const messagesList = document.getElementById("messagesList");
    messagesList.innerHTML = ""; // Clear the content of the messagesList
}