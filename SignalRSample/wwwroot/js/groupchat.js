const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/groupchathub")
    .build();

const groupMessages = {};
connection.on("ReceiveMessage", function (user, message, groupName) {
    // Ensure the groupMessages object has a property for the specific group
    if (!groupMessages[groupName]) {
        groupMessages[groupName] = [];
    }

    // Add the message to the group's message list
    groupMessages[groupName].push(`${user}: ${message}`);

    // Check if the current group matches the active group, then update the UI
    const activeGroup = document.getElementById("groupInput").value;
    if (groupName === activeGroup) {
        updateMessagesList(groupName);
    }
});

function updateMessagesList(groupName) {
    const messagesList = document.getElementById('messagesList');
    if (messagesList) {
        // Clear existing messages
        messagesList.innerHTML = "";

        // Add messages for the specific group
        groupMessages[groupName].forEach((message) => {
            const listItem = document.createElement("li");
            listItem.textContent = message;
            messagesList.appendChild(listItem);
        });
    }
}


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
    const groupName = document.getElementById("groupInput").value;
    messagesList.innerHTML = ""; // Clear the content of the messagesList
    groupMessages[groupName] = [];
}