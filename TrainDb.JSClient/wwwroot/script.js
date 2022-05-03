let Employees = [];
let connection = null;
getdata();
setupSignalR();

let employeeIdToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:51716/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("EmployeeCreated", (user, message) => {
        getdata();
    });

    connection.on("EmployeeDeleted", (user, message) => {
        getdata();
    });
    connection.on("EmployeeUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    await fetch('http://localhost:51716/employee')
        .then(x => x.json())
        .then(y => {
            Employees = y;
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    Employees.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>"
        + t.managerId + "</td><td>" + `<button type="button" onclick="remove(${t.id})">Delete</button>` + `<button type="button" onclick="showupdate(${t.id})">Update</button>`+ "</td></tr>";
    });
}

function showupdate(id) {
    document.getElementById('nametoupdate').value = Employees.find(t => t['id'] == id)['name'];
    document.getElementById('manageridtoupdate').value = Employees.find(t => t['id'] == id)['managerId'];
    document.getElementById('updateformdiv').style.display = 'flex';
    employeeToUpdate = id;
}

function remove(id) {
    fetch('http://localhost:51716/employee/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function create() {
    let _name = document.getElementById('name').value;
    console.log(name);
    let _managerid = document.getElementById('managerid').value;
    console.log(managerid);

    fetch('http://localhost:51716/employee', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {Name: _name, managerId: _managerid }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
function update() {
    document.getElementById('updateformdiv').style.display = "none";
    alert(document.getElementById('nametoupdate').value);
    let _name = document.getElementById('nametoupdate').value;
    console.log(name);
    let _managerid = document.getElementById('manageridtoupdate').value;
    console.log(managerid);

    fetch('http://localhost:51716/employee', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { id: employeeToUpdate, name: _name, managerId: _managerid}),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}