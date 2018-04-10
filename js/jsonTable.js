
function CreateTableFromJSON(uid_arr, country) {

    var data = completedArrayTemp;

    var col = [];
    for (var i = 0; i < data.length; i++) {
        for (var key in data[i]) {
           
               
                if (col.indexOf(key) === -1) {
                    col.push(key);
                }
            
        }
    }

    // CREATE DYNAMIC TABLE.
    var table = document.createElement("table");
    table.id = "jsonTab";
    // CREATE HTML TABLE HEADER ROW USING THE EXTRACTED HEADERS ABOVE.

    var tr = table.insertRow(-1);                   // TABLE ROW.

    for (var i = 0; i < col.length; i++) {
        var th = document.createElement("th");      // TABLE HEADER.
        th.innerHTML = col[i];
        tr.appendChild(th);
    }
   
   

    // ADD JSON DATA TO THE TABLE AS ROWS.
    for (var i = 0; i < data.length; i++) {
        if (country == "Choose a country" && contains(uid_arr, data[i].UID)) {
                tr = table.insertRow(-1);

                for (var j = 0; j < col.length; j++) {
                    var tabCell = tr.insertCell(-1);
                    tabCell.class = "xx";
                    tabCell.innerHTML = data[i][col[j]];
                }
            
        }
        else if (data[i].CategoryName == country && contains(uid_arr,data[i].UID)) {
            tr = table.insertRow(-1);

            for (var j = 0; j < col.length; j++) {
                var tabCell = tr.insertCell(-1);
                tabCell.class = "xx";

                tabCell.innerHTML = data[i][col[j]];
            }
        }
  
    }

    // FINALLY ADD THE NEWLY CREATED TABLE WITH JSON DATA TO A CONTAINER.
    var divContainer = document.getElementById("reqs");
    divContainer.innerHTML = "";
    divContainer.appendChild(table);
}