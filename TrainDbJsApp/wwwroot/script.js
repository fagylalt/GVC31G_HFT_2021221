fetch('http://localhost:51716/manager')
    .then(x => x.json())
    .then(y => console.log(y));