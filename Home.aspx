<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<html>
<head runat="server">
    <title>Land Use/Land Cover Inventory for Africa</title>
       

</head>
<body>
    <meta name="google-signin-client_id" content="140425635617-nkebib8gikq31h7ju0o3qk27c3s681dd.apps.googleusercontent.com">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href='//fonts.googleapis.com/css?family=Open+Sans:400italic,400,600,700' rel='stylesheet' type='text/css'>
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="shortcut icon" href="favicon.png">
    <link rel="stylesheet" type="text/css" href="//js.arcgis.com/3.8/js/esri/css/esri.css"
        media="screen" />
    <link href="css/style_poly_markers.css" rel="stylesheet">
    <script>
        // helpful for understanding dojoConfig.packages vs. dojoConfig.paths:
        // http://www.sitepen.com/blog/2013/06/20/dojo-faq-what-is-the-difference-packages-vs-paths-vs-aliases/
        var dojoConfig = {
            paths: {
                extras: location.pathname.replace(/\/[^/]+$/, "") + "/extras"
            }
        };
        var adminLoggedIn = 0;


    </script>
     
    <link rel="stylesheet" href="css/jquery-ui.css">
    <script src="js/users.js"></script>
        <script src="js/reportedProblems.js"></script>

    <script src="https://apis.google.com/js/platform.js" async defer></script>
    <script src="https://apis.google.com/js/client:platform.js?onload=renderButton" async defer></script>
    <script src="js/gLogin.js"></script>
    <script>
        //Whenever a user logs in, this ,method adds user details to the json file
        function addUserEmail(role, em, nme) {
            if (em != null && em != undefined) {
                var jarr = {
                    "Role": role,
                    "Email": em,
                    "FullName": nme
                }
                var j = JSON.stringify(jarr);
                document.getElementById('<%=hdnUser.ClientID%>').value = j;
                PageMethods.updateUserJson(document.getElementById('<%=hdnUser.ClientID%>').value);
            }
        }
        function showMap() {
            document.getElementById("loader").style.display = "none";
            document.getElementById("map").style.display = "block";
            document.getElementById("map").style.opacity = "1";

        }

        function displayMenu() {
            if (document.getElementById("respMenu").style.display == "none")
                document.getElementById("respMenu").style.display = "block";
            else
                document.getElementById("respMenu").style.display = "none";

        }
        function hidePanel() {
            document.getElementById("accordionholder").style.display = "none";
        }
    </script>
    <script src="js/FileSaver.js"></script>
    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script src="js/jquery-ui.js"></script>
    <script src="//js.arcgis.com/3.8/" type="text/javascript"></script>
    <script src="js/sortedcompleted.js"></script>
    <script src="data/CategoryArray.js"></script>
    <script src="js/locationcentroids.js"></script>
    <script src="data/countryPoly.js"></script>
    <script src="js/arcgis_poly_markers3.js" type="text/javascript"></script>
    <link href="css/Popup.css" rel="stylesheet">
    <script src="js/shadowbox.js" type="text/javascript"></script>
    <script src="js/jsonTable.js" type="text/javascript"></script>
    <link href="css/shadowbox.css" rel="stylesheet" type="text/css" />
    <link href="css/addAdmin.css" rel="stylesheet" type="text/css" />
    <script>

        var testvar;
        var gClusters;
        var email_str;
        var submitted = 0;
        var loggedIn = 0;
        var mail_id;
        var sid;
    </script>
    <script>

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        $(function () {

            $("#accordion").accordion({ heightStyle: "fill" });
            document.getElementById("accordionholder").style.display = "none";
        });
        $(window).resize(function () {
            $("#accordion").accordion("refresh");
        });
        $(function () {
            Shadowbox.init({
                continuous: false,
                counterType: "none",
                modal: true,
                enableKeys: false
            });
        });
        //Opens a pop up(shadow box) for the information about the website
        function about() {
            Shadowbox.open({
                content: '<div style="color:black;">In the context of the AfriGEOSS Working Group on Land Cover for Africa (WGLCA), the Ecological Monitoring Center (CSE) of Senegal, SERVIR Science Coordination Office and SERVIR West Africa joined efforts to develop a dynamic Land Cover Inventory for Africa<br /><br />' +
                         'This inventory is a collection of information regarding the multiple efforts on land cover and land use products for the continent of Africa. The input data were originally collected through the members of the AfriGEOSS WGLCA Executive Board. The purpose of this inventory is to have a complete understanding of the available  land cover products  in the continent,  to raise awareness and promote use of the data that exists and avoid duplication of efforts.<br/><br/>' +
                         'We encourage users to update information about land cover datasets available for the region. Please enter new records by signing-in into this portal. ' +
                         '<p>App Development: SERVIR, 2017<p>' +
                         '</div>',
                player: "html",
                title: "About website",
                height: "389px"
            });
        }


        //When the "+" button is clicked  by admin to add data to a country
        function addMoreData() {
                Shadowbox.open({
                    content: '<div id="welcome-msg"> ' +
                                ' <table class="addmore_table">' +

                                  ' <tr><td>Country:</td></tr><tr><td><span  id="ctry" class="ctry"><b>' + document.getElementById("ctry_hidden").innerHTML + '</b></span></tr>' +

                                ' <tr><td>Status:<br><span style="font-size:12px;">(Condition of development for the map)<span></td></tr><tr><td>' +
                                ' <select id="dd" class="status"> ' +
                                  ' <option value="completed">Completed</option> ' +
                                  ' <option value="inprogress">In progress</option> ' +
                                                                    ' <option value="planned">Planned</option> ' +

                                ' </select></td></tr>' +
                                ' <tr><td>Title:</td></tr><tr><td><input placeholder="Name of the land cover dataset" id="title" class="textbox" type="text"/></td><td class="Req" style="color:red;font-size:25px;" hidden>*</td><td id="Row1" style="padding:0;margin:0;color:red;" hidden><span id="errorMsgTitle" ></span></td></tr>' +

                                ' <tr><td>Map Year:</td></tr><tr><td><textarea placeholder="Year that the map represents on the ground " id="map_year" cols="20" rows="1" class="textboxcite" type="text"></textarea></td></tr>' +
                                                                ' <tr><td>Released Year:</td></tr><tr><td><textarea placeholder="Year of publication. It may be different from Map year. " id="release" cols="20" rows="1" class="textboxcite" type="text"></textarea></td></tr>' +

                                ' <tr><td>Organization:</td></tr><tr><td><textarea placeholder="Name of the institution and/or organization that generated the land cover map" id="org" cols="20" rows="1" class="textboxcite" type="text"></textarea></td></tr>' +

                                ' <tr><td>Number of classes:</td></tr><tr><td><textarea placeholder="Number of land cover classes available in the map" id="cls" cols="20" rows="1" class="textboxcite" type="text"></textarea></td></tr>' +

                                ' <tr><td>Data Source:</td></tr><tr><td><textarea placeholder="Original dataset on which the map is based (i.e. Landsat satellite images)" id="ds" cols="20" rows="1" class="textboxcite" type="text"></textarea></td></tr>' +

                              
                                ' <tr><td>Notes:</td></tr><tr><td><textarea placeholder="Any additional information of relevance about the land cover map. If dataset available online please provide link here" id="notes" cols="20" rows="4" class="textboxcite" type="text"></textarea></td></tr>' +

                                ' <tr><td>Point of contact:</td></tr><tr><td><input placeholder="Point of contact name" id="poc" class="textbox" type="email"/></td></tr>' +

                                ' <tr><td>Email:</td></tr><tr><td><textarea placeholder="email of the person that can provide further information about this land cover map" id="email" cols="20" rows="1" class="textboxcite" type="text"></textarea></td><td id="Row2" style="padding:0;margin:0;color:red;" hidden><span id="errorMsgPoc" ></span></td></tr>' +
                                ' <tr><td>Phone Number:</td></tr><tr><td><input placeholder="Point of contact phone number" id="ph_num" class="textbox" type="text"/></td><td id="Row3" style="padding:0;margin:0;color:red;" hidden><span id="errorMsgPhn" ></span></td></tr>' +
                                ' <tr><td>How to cite:</td></tr><tr><td><textarea placeholder="Enter how users should reference this map" id="cite" cols="20" rows="4" class="textboxcite" type="text"></textarea></td></tr>' +
                                ' <tr><td></td></tr><tr><td><button type="button" class="btn" onclick="AddJsonData()">Submit data!</button></tr>' +

                                ' </table>' +
                             '</div>',
                    player: "html",
                    title: "Add More Data",
                    height: "565px"
                });

        }
        //this method is used inside addMoreData() to update the json file with new data
        function AddJsonData() {

            if (document.getElementById("title").value != "") {
                if (valid_Id(document.getElementById("email").value)) {
                    if (validatePhone(document.getElementById("ph_num")) || document.getElementById("ph_num").value == "") {
                        var newCatId = "";
                        for (var c = 0; c < activityCats.length; c++) {
                            if (activityCats[c].CategoryName == document.getElementById("ctry_hidden").innerHTML) {
                                newCatId = activityCats[c].CategoryID;
                                break;
                            }
                        }

                        if (document.getElementById("map_year").value == "") document.getElementById("map_year").value = "Not specified";
                        if (document.getElementById("org").value == "") document.getElementById("org").value = "Not specified";
                        if (document.getElementById("ds").value == "") document.getElementById("ds").value = "Not specified";
                        if (document.getElementById("notes").value == "") document.getElementById("notes").value = "Not specified";
                        if (document.getElementById("poc").value == "") document.getElementById("poc").value = "Not specified";
                        if (document.getElementById("email").value == "") document.getElementById("email").value = "Not specified";

                        if (document.getElementById("cite").value == "") document.getElementById("cite").value = "Not specified";
                        if (document.getElementById("cls").value == "" || isNaN(document.getElementById("cls").value)) document.getElementById("cls").value = "0";
                        if (document.getElementById("release").value == "" || isNaN(document.getElementById("release").value)) document.getElementById("release").value = "0";

                        if (document.getElementById("ph_num").value == "") document.getElementById("ph_num").value = "000-000-0000";
                        var newUID = Math.random().toString(36).substring(2) + (new Date()).getTime().toString(36);

                        var today = new Date();
                        var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
                        var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
                        var dateTime = date + ' ' + time;
                        var newData = {
                            "UID": newUID,
                            "Title": document.getElementById("title").value,
                            "CategoryName": document.getElementById("ctry_hidden").innerHTML,
                            "CategoryID": [],
                            "MapYear": document.getElementById("map_year").value,
                            "Organization": document.getElementById("org").value,
                            "NumberOfClasses": [],
                            "DataSource": document.getElementById("ds").value,
                            "Status": document.getElementById("dd").options[document.getElementById("dd").selectedIndex].text,
                            "ReleasedYear": parseInt(document.getElementById("release").value),
                            "Notes": document.getElementById("notes").innerHTML,
                            "PointOfContactName": document.getElementById("poc").value,
                            "Email": document.getElementById("email").value,
                            "PhoneNumber": document.getElementById("ph_num").value,
                            "HowToCite": document.getElementById("cite").value,
                            "LastUpdatedBy": document.getElementById('uemail').innerHTML.split(':')[1].trim(),
                            "LastUpdatedTime":dateTime
                        }

                        newData["CategoryID"].push(parseInt(newCatId));
                        newData["NumberOfClasses"].push(parseInt(document.getElementById("cls").value));

                        var recent = document.getElementById('uemail').innerHTML.split(':');
                        for (var i = 0; i < recent.length; i++) {
                            recent[i] = recent[i].trim();
                        }

                        var fl = 0;

                        var toAdd = {

                            "UID": newUID,
                            "Email": recent[1],
                            "FullName": document.getElementById('ufullname').innerHTML
                        }
                        var str = JSON.stringify(newData);
                        document.getElementById('<%=hdn.ClientID%>').value = str;
                        if (adminLoggedIn == 0) {
                            var ta = JSON.stringify(toAdd);
                            if (newlyAdded.length == 0) fl = 1;
                            PageMethods.updateNewlyAddedData(fl, ta);
                            newlyAdded.push(toAdd);
                            PageMethods.updateJsonTemp(document.getElementById('<%=hdn.ClientID%>').value);
                            completedArrayTemp.push(newData);
                            alert("Request sent to Admin for approval!");

                        }
                        else {
                            PageMethods.updateJson(document.getElementById('<%=hdn.ClientID%>').value);
                            completedArray.push(newData);
                            map.removeLayer(map.getLayer("completed"));
                            map.removeLayer(map.getLayer("planned"));
                            map.removeLayer(map.getLayer("inprogress"));
                            catArray = [];
                            planned = [];
                            inprogress = [];
                            completed = [];
                            total = [];

                            expandcompletedArray();

                            gClusters(completedArray, 50);
                            populatePanelByCountry(document.getElementById("ctry_hidden").innerHTML);
                           
                        }

                        document.getElementById("title").value = "";
                        document.getElementById("map_year").value = "";
                        document.getElementById("org").value = "";
                        document.getElementById("ds").value = "";
                        document.getElementById("dd").selectedIndex = 0;
                        document.getElementById("release").value = "";
                        document.getElementById("notes").value = "";
                        document.getElementById("poc").value = "";
                        document.getElementById("email").value = "";
                        document.getElementById("ph_num").value = "";
                        document.getElementById("cite").value = "";
                        document.getElementById("cls").value = "";
                        Shadowbox.close();


                    }

                    else {
                        $(".Req").hide();
                        $("#errorMsgPhn").html("Please enter a valid phone number");
                        $("#Row3").show();
                        $("#Row2").hide();

                        $("#Row1").hide();
                        $("#ph_num").focus();
                    }
                }
                else {
                    $(".Req").hide();
                    $("#errorMsgPoc").html("Please enter a valid email address for point of contact");
                    $("#Row2").show();
                    $("#Row3").hide();

                    $("#Row1").hide();
                    $("#email").focus();
                }
            } else {
                $(".Req").show();
                $("#errorMsgTitle").html("Please enter a title");
                $("#Row1").show();
                $("#Row2").hide();
                $("#Row3").hide();

                $("#title").focus();

            }

        }
        function removeFromObj(num, arr) {
            for (var i in arr) {
                if (arr[i].UID == num)
                    arr.splice(i, 1);
            }
        }
        //this method is called when admin clicks on "Approve" button in ViewRequests
        function approveData(uid, title, catname, catid, mapyear, org, cls, ds, status, release, notes, poc, email, phnum, cite, lub,lut) {
            var newData = {
                "UID": uid,
                "Title": title,
                "CategoryName": catname,
                "CategoryID": [],
                "MapYear": mapyear,
                "Organization": org,
                "NumberOfClasses": [],
                "DataSource": ds,
                "Status":  decodeURIComponent(status),
                "ReleasedYear": parseInt(release),
                "Notes": notes,
                "PointOfContactName": poc,
                "Email": email,
                "PhoneNumber": phnum,
                "HowToCite": cite,
                "LastUpdatedBy": lub,
                "LastUpdatedTime": decodeURIComponent(lut)

            }

            newData["CategoryID"].push(parseInt(catid));
            newData["NumberOfClasses"].push(parseInt(cls));


            var str = JSON.stringify(newData);
            document.getElementById('<%=hdn.ClientID%>').value = str;
            PageMethods.updateJson(document.getElementById('<%=hdn.ClientID%>').value);
            completedArray.push(newData);
            map.removeLayer(map.getLayer("completed"));
            map.removeLayer(map.getLayer("planned"));
            map.removeLayer(map.getLayer("inprogress"));
            catArray = [];
            planned = [];
            inprogress = [];
            completed = [];
            total = [];
            expandcompletedArray();

            gClusters(completedArray, 50);
            populatePanelByCountry(document.getElementById("ctry_hidden").innerHTML);

            //map.destroy();
            //gToggle.destroy();
            //initMap();
            PageMethods.DeleteFromTemp(uid);
            removeFromObj(uid, completedArrayTemp);
            var row = document.getElementById(uid);
            row.parentNode.removeChild(row);
            $('#statusMsg').html("<b style='color:green'>Approved record with UID " + uid + "</b>");


        }
        //Sends the data to Unapproved list( this data can be viewed in ViewRequests page)
        function unapproveData(uid) {
         
            for (i=0;i<sortedcompleted.length;i++) {
                if (sortedcompleted[i].UID == uid) {
                    PageMethods.updateJsonTemp(JSON.stringify(sortedcompleted[i]));
                    

                   

                   completedArrayTemp.push(sortedcompleted[i]);
                    break;
                }
            }
            var fl = 0;
            if (newlyAdded.length == 0) fl = 1;
            var recent = document.getElementById('uemail').innerHTML.split(':');
            for (var i = 0; i < recent.length; i++) {
                recent[i] = recent[i].trim();
            }
            var toAdd = {

                "UID": uid,
                "Email": recent[1],
                "FullName": "admin unapproved"
            }
           
            PageMethods.updateNewlyAddedData(fl, JSON.stringify(toAdd));
            newlyAdded.push(toAdd);
            PageMethods.DeleteFromOriginal(uid);
            removeFromObj(uid, completedArray);
            map.removeLayer(map.getLayer("completed"));
            map.removeLayer(map.getLayer("planned"));
            map.removeLayer(map.getLayer("inprogress"));
            catArray = [];
            planned = [];
            inprogress = [];
            completed = [];
            total = [];
            expandcompletedArray();

            gClusters(completedArray, 50);
            populatePanelByCountry(document.getElementById("ctry_hidden").innerHTML);

            alert("Unapproved this record.. View requests to approve it!");
            Shadowbox.close();
        }
        //this method is called when admin clicks on "Discard" button in ViewRequests
        function discardData(uid) {
            PageMethods.DeleteFromnewlyAddedData(uid);
            PageMethods.DeleteFromTemp(uid);
            $('#statusMsg').html("<b style='color:red'>Discarded record with UID " + uid + "</b>");
            removeFromObj(uid, newlyAdded);
            removeFromObj(uid, completedArrayTemp);
            map.removeLayer(map.getLayer("completed"));
            map.removeLayer(map.getLayer("planned"));
            map.removeLayer(map.getLayer("inprogress"));
            catArray = [];
            planned = [];
            inprogress = [];
            completed = [];
            total = [];
            expandcompletedArray();
            var row = document.getElementById(uid);
            row.parentNode.removeChild(row);
            gClusters(completedArray, 50);
            populatePanelByCountry(document.getElementById("ctry_hidden").innerHTML);

        }
        //Deletes the data permanently from the files
        function deleteData(_uid) {
            if (confirm('Are you sure to delete this data?')) {
                PageMethods.DeleteFromOriginal(_uid);
                removeFromObj(_uid, completedArray);
                map.removeLayer(map.getLayer("completed"));
                map.removeLayer(map.getLayer("planned"));
                map.removeLayer(map.getLayer("inprogress"));
                catArray = [];
                planned = [];
                inprogress = [];
                completed = [];
                total = [];
                expandcompletedArray();

                gClusters(completedArray, 50);
                populatePanelByCountry(document.getElementById("ctry_hidden").innerHTML);

                alert('Deleted successfully')
                Shadowbox.close();
            }
        }   
        function reportProblemPopup() {
            $('.modal_reportproblems').show();
        }
        function submitReportProblem() {
            var problem = document.getElementById("enter_problem").value;
            var reportedBy = "";
            if (problem != null) {
                var uniqueId = Math.random().toString(36).substring(2) + (new Date()).getTime().toString(36);
                if (document.getElementById('uemail').innerHTML == "You are not logged in!") {
                    reportedBy = document.getElementById('emailAddress').value;
                }
                else
                {
                    reportedBy=document.getElementById('uemail').innerHTML.split(':')[1].trim();
                }
                var jarr = {
                    "PID": uniqueId,
                    "Problem": problem,
                    "Status": "Open",

                    "reportedBy": reportedBy
                }
                reportedProblems.push(jarr);
                var j = JSON.stringify(jarr);
                PageMethods.updateProblemsJson(j);
            }
            $('.modal_reportproblems').hide();
            document.getElementById("enter_problem").value = "";
        }

        function closeStatus(pid, problem, reportedby) {
            for (var i in reportedProblems) {
                if (reportedProblems[i].PID == pid) {
                    reportedProblems[i].Problem = decodeURIComponent(problem);
                    reportedProblems[i].Status = "Closed";
                    reportedProblems[i].reportedBy = reportedby;
                }
            }
            var newData = {
                "PID": pid,
                "Problem": decodeURIComponent(problem),
                "Status": "Closed",
                "reportedBy": reportedby
            }

            var str = JSON.stringify(newData);
            document.getElementById('<%=hdn.ClientID%>').value = str;
            PageMethods.updateProblemJson(pid, decodeURIComponent(problem), "Closed", reportedby);
            var row = document.getElementById(pid);
            row.parentNode.removeChild(row);

            $('#statusMesg').html("<b style='color:green'>Closed record with PID " + pid + "</b>");


        }
        function openStatus(pid, problem, reportedby) {
            for (var i in reportedProblems) {
                if (reportedProblems[i].PID == pid) {
                    reportedProblems[i].Problem = decodeURIComponent(problem);
                    reportedProblems[i].Status = "Open";
                    reportedProblems[i].reportedBy = reportedby;
                }
            }
            var newData = {
                "PID": pid,
                "Problem": decodeURIComponent(problem),
                "Status": "Open",
                "reportedBy": reportedby
            }

            var str = JSON.stringify(newData);
            document.getElementById('<%=hdn.ClientID%>').value = str;
            PageMethods.updateProblemJson(pid, decodeURIComponent(problem), "Open", reportedby);
            var row = document.getElementById(pid);
            row.parentNode.removeChild(row);
            $('#statusMesg').html("<b style='color:green'>Reopened record with PID " + pid + "</b>");


        }
     
        function reportProblem(uid) {

        }

        //Updates existing records when the admin edits and submits the data
        function updateData(uid) {
            var today = new Date();
            var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
            var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
            var dateTime = date + ' ' + time;
            document.getElementById('<%=hdnData.ClientID%>').value = uid;
            var title = document.getElementById("ea").value;

            var mapyear = document.getElementById("ec").value;
            var org = document.getElementById("ed").value;
            var cls = document.getElementById("ee").value;
            var ds = document.getElementById("ef").value;
            var status = document.getElementById("eg").options[document.getElementById("eg").selectedIndex].text;
            var release = document.getElementById("eh").value;
            var notes = document.getElementById("ek").value;
            var poc = document.getElementById("el").value;
            var email = document.getElementById("em").value;
            var phnum = document.getElementById("en").value;

            var cite = document.getElementById("eo").value;
            var lub = document.getElementById('uemail').innerHTML.split(':')[1].trim();
            var lut = document.getElementById('uemail').innerHTML.split(':')[1].trim();
            for (var i = 0; i < completedArray.length; i++) {
                if (completedArray[i].UID == uid) {
                    completedArray[i].Title = title;
                    completedArray[i].MapYear = mapyear;
                    completedArray[i].Organization = org;
                    if (!isNaN(cls))
                        completedArray[i].NumberOfClasses = parseInt(cls);
                    else
                        completedArray[i].NumberOfClasses = 0;
                    completedArray[i].DataSource = ds;
                    completedArray[i].Status = status;
                    if (!isNaN(release))
                        completedArray[i].ReleasedYear = parseInt(release);
                    else

                        completedArray[i].ReleasedYear = 0;
                    completedArray[i].Notes = notes;
                    completedArray[i].PointOfContactName = poc;
                    completedArray[i].Email = email;
                    completedArray[i].PhoneNumber = phnum;
                    completedArray[i].HowToCite = cite;
                    completedArray[i].LastUpdatedBy = lub;
                    completedArray[i].LastUpdatedTime = dateTime;
                }
            }
            PageMethods.updateExistingData(document.getElementById('<%=hdnData.ClientID%>').value, title, mapyear, org, cls, ds, status, release, notes, poc, email, phnum, cite,lub);
            map.removeLayer(map.getLayer("completed"));
            map.removeLayer(map.getLayer("planned"));
            map.removeLayer(map.getLayer("inprogress"));
            catArray = [];
            planned = [];
            inprogress = [];
            completed = [];
            total = [];
            expandcompletedArray();

            gClusters(completedArray, 50);
            populatePanelByCountry(document.getElementById("ctry_hidden").innerHTML);

            alert("Data is updated successfully");
           
            Shadowbox.close();
        }
        //$(document).ready(function () {
        //    initMap();
        //});
    </script>
    <style>
       

        .icon-content {
            display: none;
            position: absolute;
            background-color: #f9f9f9;
            min-width: 160px;
            overflow: auto;
            box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
            z-index: 1;
        }

            .icon-content a {
                color: black;
                padding: 12px 16px;
                text-decoration: none;
                display: block;
            }

        .header {
            width: 100%;
            height: 150px;
            border-bottom-style: solid;
        }

        #loader {
            position: absolute;
            left: 50%;
            top: 50%;
            z-index: 1;
            width: 150px;
            height: 150px;
            margin: -75px 0 0 -75px;
            border: 16px solid white;
            border-radius: 50%;
            border-top: 16px solid black;
            width: 120px;
            height: 120px;
            -webkit-animation: spin 2s linear infinite;
            animation: spin 2s linear infinite;
        }

        @-webkit-keyframes spin {
            0% {
                -webkit-transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(360deg);
            }
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        #ea, #eb, #ec, #ed, #ee, #ef, #eh, #ek, #el, #em, #en, #eo {
            width: 300px;
            font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
            font-size: 15px;
        }

        .data_d {
            text-align: center;
            border: dotted;
            padding: 5px;
        }

        #Row1, #Row2, #Row3 {
            font-size: 12px;
        }

        #etable {
            margin-left: 18%;
        }

        .d {
            height: 40px;
        }

        .addmore_table {
            font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
            font-size: 15px;
            margin-left: 18%;
        }

        .status{
            margin-top: 5px;
            border: 3px groove;
            outline: 0;
            height: 30px;
            width: 300px;
            margin-bottom: 5px;
             padding: 8px 16px 8px;
    border-radius: 10px;
    height: 50px;
        }
        .textboxcite  {
            margin-top: 5px;
            border: 3px groove;
            outline: 0;
            height: 40px;
            width:300px;
            margin-bottom: 5px;
             padding: 8px 16px 8px;
    border-radius: 10px;
    height: 75px;
        }

        .textbox {
            margin-top: 5px;
            border: 3px groove;
            outline: 0;
            height: 25px;
            width: 300px;
            margin-bottom: 5px;
            font-size: 12px;
             padding: 8px 16px 8px;
    border-radius: 10px;
    height: 50px;
    font-family:monospace;
        }

       

       #mygraphics_layer path {
            stroke-linejoin: round;
            stroke-linecap: round;
            fill-rule: evenodd;
            stroke: rgb(56, 173, 115);
            stroke-opacity: 1;
            stroke-width: 1px;
            fill: rgb(31, 96, 64);
            fill-opacity: 0.8;
        }

            #mygraphics_layer path:hover {
                fill: rgb(215, 180, 142);
                fill-opacity: 0.8;
            }

            #mygraphics_layer  path:hover {
                cursor: pointer;
                animation-duration: 0.2s;
                animation-name: highlight;
                animation-timing-function: linear;
                animation-fill-mode: forwards;
                -webkit-animation-duration: 0.2s;
                -webkit-animation-name: highlight;
                -webkit-animation-timing-function: linear;
                -webkit-animation-fill-mode: both;
                border-color: blue;
            }

        .vertical {
            position: absolute;
            border-left: 1px solid #848484;
            border-right: 1px solid #848484;
            background-color: #dadada;
            width: 4px;
            height: 100%;
            padding: 0 !important;
            margin: 0;
            z-index: 30;
        }

        .vertical {
            background-image: url(form/images/sliderVertical.png);
            background-repeat: repeat-x;
            background-position: 0 -20px;
            border-color: black;
            background-color: black;
            right: 0px;
            position: absolute;
            z-index: 100;
            display: none;
        }

        .handleContainer {
            position: relative;
            width: 100%;
            height: 100%;
        }

        .handle {
            width: 24px;
            height: 65px;
            margin: -16px 0 0 -22px;
            position: absolute;
            z-index: 30;
            top: 50%;
            left: 50%;
            background-color: #dadada;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            border: 1px solid #848484;
            background-image: url("../images/vClose1.png");
            background-repeat: no-repeat;
            background-position: center center;
            border-color: transparent;
            background-color: transparent;
            cursor: pointer;
        }

            .handle:hover {
                opacity: 0.6;
            }

        .ui-accordion .ui-accordion-header {
            color: white;
            border: black 2px solid;
            border-top-right-radius: 0px;
            border-top-left-radius: 0px;
            border-bottom-right-radius: 0px;
            border-bottom-left-radius: 0px;
            margin: 0px 0 0 0;
            border-left: 0px;
            text-transform: uppercase;
            padding: 0 0 0 2.2em;
            line-height: 1.1;
        }

        #map {
            position: absolute;
            top: 148px;
            bottom: 0px;
        }

        #BasemapToggle {
            top: auto;
            position: absolute;
            left: 20px;
            z-index: 39;
            top: 10px;
        }

        #btninfo {
            display: none;
            position: relative;
            float: left;
            width: 40px;
            z-index: 98;
            background-color: #00ACEC;
            -webkit-border-radius: 0 0 8px 8px;
            border-radius: 0 0 8px 8px;
            padding: 10px;
        }

        #Body {
            padding-top: 0px !important;
        }

        .map .container {
            width: 100% !important;
        }

        div#welcome-msg {
            color: #fff;
            font-size: 14px;
            line-height: 20px;
            margin: 10px;
        }

        button, button:hover {
            background-image: none;
            color: white;
            background-color: black;
            padding: 8px 16px 8px;
            float:right;
            border:3px;
            border-radius:10px;
        }

        #horiz_menu {
            list-style-type: none;
            margin: 0;
            padding: 0;
            overflow: hidden;
            background-image: url('images/header.jpg');
            background-size: 100%;
            border-bottom-color: darkgoldenrod;
            border-bottom: solid;
        }

        .hmenu {
            float: right;
        }

            .hmenu a {
                display: block;
                color: white;
                text-align: center;
                padding: 0.5vw 0.6vw;
                text-decoration: none;
            }

                .hmenu a:hover {
                    background-color: #111;
                }

        #links {
            right: 20px;
            position: absolute;
        }

        .iconResponsive {
            display: none;
            color: white;
            font-size: 30px;
        }

        .iconHome {
            display: none;
        }
        #toshow{
            display:none;
        }
      
        @media only screen and (max-width: 700px) {
         #ea, #eb, #ec, #ed, #ee, #ef, #eh, #ek, #el, #em, #en, #eo {
            width: 200px;
            font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
            font-size: 15px;
        }
            .status  {
            margin-top: 5px;
            border: 3px groove;
            outline: 0;
            height: 30px;
            width: 200px;
            margin-bottom: 5px;
        }
             .textboxcite  {
            margin-top: 5px;
            border: 3px groove;
            outline: 0;
            height: 40px;
            width: 200px;
            margin-bottom: 5px;
        }

        .textbox {
            margin-top: 5px;
            border: 3px groove;
            outline: 0;
            height: 25px;
            width: 200px;
            margin-bottom: 5px;
            font-size: 12px;
            font-family:monospace;
        }
          
            #horiz_menu {
            border-bottom:none;
            height:48px;
            }
           
           
            .hmenu a {
                display: none;
            }

            .iconResponsive {
                float: right;
                display: block;
            }

            .iconHome {
                display: block;
                float: right;
                margin-top:5px;
                padding: 0.5vw 0.6vw;
            }


            #map {
                top: 48px;
            }

            #accordionHolder {
                left: 0;
                top: 0px;
                width: 100%;
            }

            #accordion {
                right: 0;
                left: 0;
                width: 100%;
            }

            .handleContainer {
                display: none;
            }

            #BasemapToggle {
                z-index: 10;
            }

            #toggleBar {
                z-index: inherit;
            }

            #tohide {
                display: none;
            }
            #toshow{
                display:block;
            }

            .header {
                height: 48px;
                border-bottom-style:none;
            }
            #ServirLogoHolder{
                display:none;
            }
        }
        @media only screen and (min-width: 750px) {
            .icon-content {
                visibility: hidden;
            }
        }
          @media only screen and (max-width: 500px) {
        #slogo{
                display:none;

        }
        #logoResp{
            display:block;
            width:50px;
            height:50px;
            top:10px;
        }
        }
    </style>
    <div style="width: 100%; height: 100%; margin: 0;">
        <!--has the header,links and logos-->
        <div class="header">
            <ul id='horiz_menu'>
                <li><a href="javascript:void(0);" class="iconResponsive" onclick="displayMenu()">&#9776;</a>
                    <div id="respMenu" style="z-index:40;margin-top:8vw;margin-left:70vw;position:absolute;right:0;" class="icon-content">
                         <a id="AddAdminsResp" href="#" onclick="AddAdmins()" style="display:none;" ><b>Add Admins</b> </a>
                        <a  id="ViewRequestsResp" href="#" onclick="ViewRequests()" style="display:none;"><b>View Requests</b></a>

                        <a href="#" onclick="about()"><b>About</b></a>
                        <span class="userContent" style="color: white;"></span>

                    </div>

                  
                </li>
                <li><a href="#" class="iconHome" onclick="hidePanel()">
                    <img src="css/images/home.png" width="30" height="30" /></a></li>
                <li style="background-image: url(images/header.jpg); background-size: cover; float: left; margin-top: 0.7vw; margin-left: 0.7vw;height:1vw;width:3vw">
                    <img id="slogo"  src="images/Servir-logo.png" /> <img id="logoResp" src="images/servir-globe.png" hidden/></li>
                <li class='hmenu'><span class="userContent" style="color: white;"></span></li>
                <li class='hmenu'>
                    <div style="margin-top: 3px" data-theme="dark" class="g-signin2" id="gSignIn" data-onsuccess="onSignIn" data-width="130px" data-height="45px">Sign in with Google</div>
                </li>
                <li id="AddAdmins" class='hmenu' hidden><a href="#" onclick="AddAdmins()"><b>Add Admins</b></a></li>
                <li id="ViewRequests" class='hmenu' hidden><a href="#" onclick="ViewRequests()"><b>View Requests</b></a></li>
                <li id="ViewProblems" class='hmenu' hidden><a href="#" onclick="ViewProblems()"><b>View Problems</b></a></li>
                <li id="ReportProblems" class='hmenu'><a href="#" onclick="reportProblemPopup()"><b>Report a Problem</b></a></li>
                <li id="importDataPop" class='hmenu' hidden><a href="#" onclick="ImportData()"><b>Bulk Data?</b></a></li>

                <li id="about" class='hmenu'><a href="#" onclick="about()"><b>About</b></a></li>
            </ul>

            <ul style="float: right; margin-top: 15px;">
                <li><b><span style="margin-right: 30px; color: black" id="uemail">You are not logged in!</span><br />
                    <span style="margin-right: 30px; color: black" id="ufullname"></span></b></li>


            </ul>
            <table id="tohide" style="margin-top: 15px; margin-left: 10px; margin-bottom: 10px;">
                <tr>
                    <td>
                        <img style="padding-right: 10px" src="images/logos/afrigeoss_lgo.png" height="60" /></td>
                    <td>
                        <img style="padding-right: 10px" src="images/logos/CILSS_logo.png" height="60" /></td>
                    <td>
                        <img style="padding-right: 10px" src="images/logos/CSE_logo.png" height="60" /></td>
                    <td colspan="15" style="width: 50vw; text-align: right"><span style="color: black; font-size: 2vw;">Land Use/Land Cover Inventory for Africa</span></td>
                </tr>
            </table>

          


        </div>

        <!--has the map-->
        <div class="sticky">
            <div id="loader" style="display: none"></div>

            <div id="map" class="map" data-dojo-type="dijit/layout/ContentPane" data-dojo-props="region:'center'">
                <div id="ServirLogoHolder" style="pointer-events: none; position: absolute; bottom: 12px; z-index: 100; width: 100%;">
                    <div style="margin-left: auto; margin-right: auto; width: 75px; text-align: center; width: 50%; max-width: 450px; white-space: nowrap;">
                        <img src="images/activity_legend.png" alt="Activity Legend" style="max-width: 70%; /* width: 18%; */ min-width: 30px; pointer-events: auto;" title="Activity Legend">
                    </div>
                </div>
                <div id="BasemapToggle">
                </div>
                <div title="Click to toggle data panel" id="toggleBar" class="vertical" style="top: 0px; position: absolute;">
                    <div class="handleContainer">
                        <div id="handle" class="handle" onclick="toggleAccordion()"></div>
                    </div>
                </div>
                <div id="accordionholder">
                    <div id="addmore">
                        <span id="accordionTitle" class="accordionTitle">My title</span>
                        <span id="addmorebutton" style="float: right">
                            <a href="#" id="addmorebutton_link" title="Add More" style="color: azure; font-size: 32px; padding-right: 10px; text-decoration: none" onclick="addMoreData();" hidden>+</a>
                        </span>
                    </div>
                    <div id="accordion" class="accordion">

                        <h3 class="completed">
                            <div style="color: #33cc33;" class="accordionSectionCounter">
                                <span id="numcompleted"></span>
                            </div>
                            <span style="bottom: 10px; left: 30px; position: relative;">Completed
                            </span>
                        </h3>
                        <div id="completedarticleholder">
                        </div>
                        <h3 class="inprogress">
                            <div style="color: #cccc00;" class="accordionSectionCounter">
                                <span id="numinprogress"></span>
                            </div>
                            <span style="bottom: 10px; left: 30px; position: relative;">In Progress
                            </span>

                        </h3>
                        <div id="inprogresssarticleholder">
                        </div>
                        <h3 class="planned">
                            <div style="color: #ffb366;" class="accordionSectionCounter">
                                <span id="numplanned"></span>
                            </div>
                            <span style="bottom: 10px; left: 30px; position: relative;">Planned
                            </span>
                        </h3>
                        <div id="plannedarticleholder">
                        </div>

                    </div>
                      <table id="toshow" style="background-color:white;height:50px;">
                <tr>
                    <td>
                        <img style="padding-right: 10px" src="images/logos/afrigeoss_lgo.png" height="40" width="80" /></td>
                    <td>
                        <img style="padding-right: 10px" src="images/logos/CILSS_logo.png" height="40" width="50"  /></td>
                    <td>
                        <img style="padding-right: 10px" src="images/logos/CSE_logo.png" height="40"  width="70"  /></td>
                    <td style="padding-left:90px;font-size:2.5vw"><b>Land Cover/Land Use Inventory for Africa</b></td>
                </tr>
            </table>
                </div>
            </div>
        </div>
    </div>
    <span id="uid_hidden" hidden>uid</span>

    <span id="ctry_hidden" hidden>country  name</span>
    <span id="ismobile" class="ismobile"></span>

    <!--Shows when a country is clicked-->
    <div id="templateholder" style="display: none;">
        <div class="article">
            <div class="articlelinkholder">
                <a class="articlelink" href="#">the title goes here</a>
            </div>
            <div class="inventoryDataSourceholder">
                <p class="inventoryMapYear">
                </p>
                <p class="inventoryOrganization">
                </p>
                <p class="inventoryDataSource">
                </p>
            </div>
            <br style="clear: both;" />
        </div>

    </div>



    <!-- The Modal for requests -->
    <div id="myModal_requests" class="modal_requests" >

        <!-- Modal content -->
        <div class="modal-content-requests">
            <span class="close" onclick="closeR()">&times;</span>
                                <h1 style="text-align:center;"><b>View requests from users</b></h1>

            <p>Following are the requests.. click on a row to approve or discard!</p>
            <select id="selectCountry">
                <option>Choose a country</option>

            </select>
            <select id="selectUser">
                <option>Choose a User</option>

            </select>
                <span id="statusMsg" style="margin-right: 30%; margin-left: 30%"></span>
            <p></p>
            <div id="reqs">
            </div>

        </div>

    </div>
        <!-- The Modal for problems -->
    <div id="myModal_problems" class="modal_problems">

        <!-- Modal content -->
        <div class="modal-content-problems">
            <span class="close" onclick="closeR()">&times;</span>
                                  <h1 style="text-align:center;"><b>View reported problems</b></h1>

            <p>Following are the problems.. click on a row to open or close!</p>
            <select id="selectStatus">
                <option>Choose a status</option>
                <option>Open</option>   
                <option>Closed</option>
            </select>
            <br />
            <br /><span id="statusMesg""></span>
            <p></p>
            <div id="probs">
            </div>

        </div>

    </div>
            <!-- The Modal for problems -->
    <div id="myModal_reportproblems" class="modal_reportproblems">

        <!-- Modal content -->
        <div class="modal-content-reportproblems">
            <span class="close" onclick="closeR()">&times;</span>
                               <h1 style="text-align:center;"><b>Report a problem</b></h1>

            <textarea class="textboxcite" style="width:450px;height:250px;" id="enter_problem" rows="7" cols="70" placeholder="Enter your problem here..."></textarea>
            <br />
           <div id="emailDiv"> <p>Enter email id:</p><input class="textbox" id="emailAddress" type="email" placeholder="Enter your email id here" required></div>
            <button style="margin-left: 0.7vw; margin-top: 0.5vw;padding:8px 16px 8px;float:right;" id="submit_problem" onclick="submitReportProblem()">Submit Problem</button>
        </div>

    </div>
            <form id="importDataForm" runat="server">

             <div id="myModal_importData"  class="modal_importData">
            <!-- Modal content -->
                <div class="modal-content-importData">
                  <span class="close" onclick="closeR()">&times;</span>
                    <h1 style="text-align:center;"><b>Bulk Data Operations</b></h1>
                    <h2>Import Data</h2>
               <div style="border: 2px solid black; border-radius: 25px; padding: 20px;">
              <h4>Instructions:</h4>
                   <ul>
                       <li>Download a template <a href="files/template.xlsx">here</a>.</li>
                       <li>Edit and save two copies of this excel file(.xlsx and .csv files).</li>
                       <li>Upload the .csv file and click on "Import" button to add data.</li>
                   </ul>
            <asp:FileUpload ID="FileImportData" runat="server"/>
            <br />
            <br />
            <input style="background-image: none;
            color: white;
            background-color: black;
            padding: 8px 16px 8px;border: 1px solid #fff;
    border-radius: 10px;" type="button" id="SubmitImportData" value="Import" runat="server" onserverclick="ImportData_Click" />

            <label id="myl" runat="server"></label>
                </div>
                        <br />
                                        <h2>Download Data</h2>

          <div style="border: 2px solid black; border-radius: 25px; padding: 20px;">
            <p>Please select a country/multiple countries so that you can download data. The downloaded file will be available in your "Downloads" folder.</p>
            <div>
                   <asp:ListBox runat="server" ID="selectCtry" SelectionMode="multiple">
                            <asp:ListItem Text="Ethiopia" Value="Ethiopia" />
                            <asp:ListItem Text="Kenya" Value="Kenya" />
                            <asp:ListItem Text="Lesotho" Value="Lesotho" />
                            <asp:ListItem Text="Malawi" Value="Malawi" />
                            <asp:ListItem Text="Mauritius" Value="Mauritius" />
                            <asp:ListItem Text="Namibia" Value="Namibia" />
                            <asp:ListItem Text="Somalia" Value="Somalia" />
                            <asp:ListItem Text="South Africa" Value="South Africa" />
                            <asp:ListItem Text="Sudan" Value="Sudan" />
                            <asp:ListItem Text="Swaziland" Value="Swaziland" />
                            <asp:ListItem Text="Tanzania" Value="Tanzania" />
                            <asp:ListItem Text="Uganda" Value="Uganda" />
                            <asp:ListItem Text="Zambia" Value="Zambia" />
                            <asp:ListItem Text="Rwanda" Value="Rwanda" />
                            <asp:ListItem Text="Algeria" Value="Algeria" />
                            <asp:ListItem Text="Libya" Value="Libya" />
                            <asp:ListItem Text="Morocco" Value="Morocco" />
                            <asp:ListItem Text="Angola" Value="Angola" />
                            <asp:ListItem Text="Benin" Value="Benin" />
                            <asp:ListItem Text="Central African republic" Value="Central African republic" />
                            <asp:ListItem Text="Cape Verde" Value="Cape Verde" />
                            <asp:ListItem Text="Cameroon" Value="Cameroon" />
                            <asp:ListItem Text="Burundi" Value="Burundi" />
                            <asp:ListItem Text="Burkina Faso" Value="Burkina Faso" />
                            <asp:ListItem Text="Botswana" Value="Botswana" />
                            <asp:ListItem Text="Chad" Value="Chad" />
                            <asp:ListItem Text="Republic Congo" Value="Republic Congo" />
                            <asp:ListItem Text="Cote Divoire" Value="Cote Divoire" />
                            <asp:ListItem Text="Djibouti" Value="Djibouti" />
                            <asp:ListItem Text="Egypt" Value="Egypt" />
                            <asp:ListItem Text="Equatorial Guinea" Value="Equatorial Guinea" />
                            <asp:ListItem Text="Guinea" Value="Guinea" />
                            <asp:ListItem Text="Eritrea" Value="Eritrea" />
                            <asp:ListItem Text="Gabon" Value="Gabon" />
                            <asp:ListItem Text="Ghana" Value="Ghana" />
                            <asp:ListItem Text="Guinea Bissau" Value="Guinea Bissau" />
                            <asp:ListItem Text="Liberia" Value="Liberia" />
                            <asp:ListItem Text="Madagascar" Value="Madagascar" />
                            <asp:ListItem Text="Mali" Value="Mali" />
                            <asp:ListItem Text="Mauritania" Value="Mauritania" />
                            <asp:ListItem Text="Mozambique" Value="Mozambique" />
                            <asp:ListItem Text="Niger" Value="Niger" />
                            <asp:ListItem Text="Nigeria" Value="Nigeria" />
                            <asp:ListItem Text="Sao Tome and Principe" Value="Sao Tome and Principe" />
                            <asp:ListItem Text="Sierra Leone" Value="Sierra Leone" />
                            <asp:ListItem Text="South Sudan" Value="South Sudan" />
                            <asp:ListItem Text="The Gambia" Value="The Gambia" />
                            <asp:ListItem Text="Togo" Value="Togo" />
                            <asp:ListItem Text="Tunisia" Value="Tunisia" />
                            <asp:ListItem Text="Western Sahara" Value="Western Sahara" />
                            <asp:ListItem Text="Zimbabwe" Value="Zimbabwe" />
                            <asp:ListItem Text="Democratic Congo" Value="Democratic Congo" />
                            <asp:ListItem Text="Comoros" Value="Comoros" />
                            <asp:ListItem Text="Seychelles" Value="Seychelles" />

                </asp:ListBox> 
                <br />
                <br />

               <input style="background-image: none;
            color: white;
            background-color: black;
            padding: 8px 16px 8px;    border: 1px solid #fff;
    border-radius: 10px;" type="button" id="SubmitExportData" value="Download" runat="server" onserverclick="ExportData_Click" />
                </div>
              </div>
                        <br />
                <h2>Update/Delete Data</h2>

                <div style="border: 2px solid black; border-radius: 25px; padding: 20px;">
               <h4>To update data:</h4>
                     <p>Edit the downloaded excel document and upload the .csv copy of the same. Click on "Update data" button to update existing data.</p>
               <h4>To delete data:</h4>
                                         <p>Leave the entries that you want to delete in the downloaded excel document and upload the .csv copy of the same. Click on "Delete data" button to delete existing data.</p>

               <asp:FileUpload ID="FileUpdateData" runat="server"/>
                <br />           
                <br />
                    <input type="hidden" runat="server" id="foruseremail" />
                                        <input type="hidden" runat="server" id="forusertime" />

                <input style="background-image: none;
            color: white;
            background-color: black;
            padding: 8px 16px 8px;    border: 1px solid #fff;
    border-radius: 10px;" type="button" id="UpdateData" value="Update data" runat="server" onserverclick="UpdateData_Click" />

                <input style="background-image: none;
            color: white;
            background-color: black;
            padding: 8px 16px 8px;    border: 1px solid #fff;
    border-radius: 10px;" type="button" id="DeleteData" value="Delete Data" runat="server" onserverclick="DeleteData_Click" />

</div>
            </div>
</div>
    <!-- Script Manager is necessary to call methods from .cs page-->
        
        <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" />
        <asp:HiddenField ID="hdnUser" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hdnData" runat="server"></asp:HiddenField>

        <asp:HiddenField ID="hdn" runat="server"></asp:HiddenField>
    </form>
</body>
</html>
