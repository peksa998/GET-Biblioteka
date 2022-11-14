"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/hub").build();


connection.on("ReservationCreated", function (reservationId, bookId, userId, bookName, userName) {
    console.log(reservationId);
    
    var table = document.getElementById("reservations-table");
    var row = table.insertRow();
    var title = row.insertCell(0);
    var user = row.insertCell(1);
    var approve = row.insertCell(2);
    var disapprove = row.insertCell(3);
    title.innerHTML = bookName;
    user.innerHTML = userName;


    var aForm = document.createElement("form");
    aForm.setAttribute('method', "post");
    aForm.setAttribute('action', "IznajmljenaKnjiga/CreateIssuedBook");

    var ri = document.createElement("input");
    ri.setAttribute('type', "hidden");
    ri.setAttribute('name', "reservationId");
    ri.value = reservationId;

    var ui = document.createElement("input");
    ui.setAttribute('type', "hidden");
    ui.setAttribute('name', "userId");
    ui.value = userId;

    var bi = document.createElement("input");
    bi.setAttribute('type', "hidden");
    bi.setAttribute('name', "bookId");
    bi.value = bookId;

    var s = document.createElement("input");
    s.setAttribute('type', "submit");
    s.setAttribute('value', "Prihvati");
    s.className = 'btn btn-primary';

    aForm.appendChild(ri);
    aForm.appendChild(ui);
    aForm.appendChild(bi);
    aForm.appendChild(s);

    approve.appendChild(aForm);


    var dForm = document.createElement("form");
    dForm.setAttribute('method', "post");
    dForm.setAttribute('action', "Rezervacija/DeleteReservation");

    var ri2 = document.createElement("input");
    ri2.setAttribute('type', "hidden");
    ri2.setAttribute('name', "reservationId");
    ri2.value = reservationId;

    var sd = document.createElement("input");
    sd.setAttribute('type', "submit");
    sd.setAttribute('value', "Odbij");
    sd.className = 'btn btn-danger';

    dForm.appendChild(ri2);
    dForm.appendChild(sd);

    disapprove.appendChild(dForm);
});


connection.on("IssuedBookCreated", function (reservationId) {
    console.log(reservationId);
    var element = document.getElementById(reservationId);
    element.parentNode.removeChild(element);
});


connection.start().then(function () {
    console.log("Connection enstablished!");
}).catch(function (err) {
    return console.error(err.toString());
});
