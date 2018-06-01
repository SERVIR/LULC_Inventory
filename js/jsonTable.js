
function CreateTableFromJSONRequests(uid_arr, country) {
    var data = completedArrayTemp;

    var col = [];
    col.push("Action");

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

    for (var i = 0; i < 3; i++) {
        var th = document.createElement("th");      // TABLE HEADER.
        th.innerHTML = col[i];
        tr.appendChild(th);
    }
   
   

    // ADD JSON DATA TO THE TABLE AS ROWS.
    for (var i = 0; i < data.length; i++) {
        if (country == "Choose a country" && contains(uid_arr, data[i].UID)) {
                tr = table.insertRow(-1);
                tr.id = data[i].UID.toString();
                for (var j = 0; j < 3; j++) {
                    var tabCell = tr.insertCell(-1);
                    tabCell.class = "xx";
                    var rep = data[i].Title.replace(' "', '');
                    var coun = data[i].CategoryName.replace(' "', '');
                    var inp = data[i].Status.replace('" ', '');
                    var tim = data[i].LastUpdatedTime.replace('" ', '');

                    if (j == 0) {
                        tabCell.innerHTML = '<button  style="display:contents;background:transparent;background-color:none;" onclick=approveData("' + data[i].UID + '","' + escape(rep) + '","' + escape(coun) + '","' + data[i].CategoryID + '","' + data[i].MapYear + '","' + data[i].Organization + '","' + data[i].MapYear + '","' + data[i].NumberOfClasses + '","' + data[i].DataSource + '","' + escape(inp) + '","' + data[i].ReleasedYear + '","' + data[i].Notes + '","' + data[i].PointOfContactName + '","' + data[i].POCEmail + '","' + data[i].POCPhoneNumber + '","' + data[i].HowToCite + '","' + data[i].LastUpdatedBy + '","' + escape(tim) + '")><img height="100%" src="approve.png"/></button><button style="display:contents;sbackground:transparent;background-color:none;" onclick=discardData("' + data[i].UID + '")><img height="100%" src="discard.png"/></button>';
                    }
                    else if (j == 2) tabCell.innerHTML = '<a href="#" onclick=ViewDataFromRequests("' +data[i].UID+ '")>' + data[i][col[j]] + '</a>';
                    else tabCell.innerHTML = data[i][col[j]];
                }
            
        }
        else if (data[i].CategoryName == country && contains(uid_arr,data[i].UID)) {
            tr = table.insertRow(-1);
            tr.id = data[i].UID.toString();
            for (var j = 0; j < 3; j++) {
                var tabCell = tr.insertCell(-1);
                tabCell.class = "xx";
                var rep = data[i].Title.replace(/['"]+/g, '');
                var coun = data[i].CategoryName.replace(' "', '');
                var inp = data[i].Status.replace(' "', '');
                var tim = data[i].LastUpdatedTime.replace('" ', '');
                if (j == 0) {
                    tabCell.innerHTML = '<button  style="display:contents;background:transparent;background-color:none;" onclick=approveData("' + data[i].UID + '","' + escape(rep) + '","' + escape(coun) + '","' + data[i].CategoryID + '","' + data[i].MapYear + '","' + data[i].Organization + '","' + data[i].MapYear + '","' + data[i].NumberOfClasses + '","' + data[i].DataSource + '","' + escape(inp) + '","' + data[i].ReleasedYear + '","' + data[i].Notes + '","' + data[i].PointOfContactName + '","' + data[i].POCEmail + '","' + data[i].POCPhoneNumber + '","' + data[i].HowToCite + '","' + data[i].LastUpdatedBy + '","' + escape(tim) + '")><img height="100%" src="approve.png"/></button><button  style="display:contents;background:transparent;background-color:none;" onclick=discardData("' + data[i].UID + '")><img height="100%" src="discard.png"/></button>';
                }
                else if (j == 2) tabCell.innerHTML = '<a href="#" onclick=ViewDataFromRequests("' +data[i].UID+ '")>' + data[i][col[j]] + '</a>';
                else tabCell.innerHTML = data[i][col[j]];
            }
        }
  
    }

    // FINALLY ADD THE NEWLY CREATED TABLE WITH JSON DATA TO A CONTAINER.
    var divContainer = document.getElementById("reqs");
    divContainer.innerHTML = "";
    divContainer.appendChild(table);
}

function CreateTableFromJSON(pid_arr) {
    var data = [];
    for (var i = 0; i < reportedProblems.length; i++) {
        
        data.push(reportedProblems[i]);
    }
   
    var col = [];
    for (var i = 0; i < reportedProblems.length; i++) {
        for (var key in reportedProblems[i]) {

            if (col.indexOf(key) === -1) {
              
                col.push(key);
            }

        }
    }
    col.push("Action");

    // CREATE DYNAMIC TABLE.
    var table = document.createElement("table");
    table.id = "jsonTabNew";
    // CREATE HTML TABLE HEADER ROW USING THE EXTRACTED HEADERS ABOVE.

    var tr = table.insertRow(-1);                   // TABLE ROW.

    for (var i = 0; i < col.length; i++) {
        var th = document.createElement("th");      // TABLE HEADER.
        th.innerHTML = col[i];
        tr.appendChild(th);
    }

    var x = 0;

    // ADD JSON DATA TO THE TABLE AS ROWS.
    for (var i = 0; i < data.length; i++) {

        if (contains(pid_arr, data[i].PID)) {
            tr = table.insertRow(-1);
            tr.id = data[i].PID.toString();
            for (var j = 0; j < col.length; j++) {
                var tabCell = tr.insertCell(-1);
                tabCell.class = "xx";
                if (j == col.length - 1)
                {
                    var p = data[i].PID.toString();
                    var rep = data[i][col[j - 3]].toString().replace(/['"]+/g, '');
                    if(data[i].Status.toString()=="Closed")
                        tabCell.innerHTML = '<button style="width:150px;" onclick=openStatus("' + p + '","' + escape(rep) + '","' + data[i][col[j - 1]].toString() + '")>Open</button>';
                    else if (data[i].Status.toString() == "Open")
                        tabCell.innerHTML = '<button  style="width:150px;"  onclick=closeStatus("' + p + '","' + escape(rep) + '","' + data[i][col[j - 1]].toString() + '")>Close</button>';


                }
                else tabCell.innerHTML = decodeURIComponent(data[i][col[j]]);
                
            }
            x++;
        }

    }

    // FINALLY ADD THE NEWLY CREATED TABLE WITH JSON DATA TO A CONTAINER.
    var divContainer = document.getElementById("probs");
    divContainer.innerHTML = "";
    divContainer.appendChild(table);
}