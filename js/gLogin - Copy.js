var OAUTHURL = 'https://accounts.google.com/o/oauth2/auth?';
var VALIDURL = 'https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=';
var SCOPE = 'https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email';
var CLIENTID = '140425635617-nkebib8gikq31h7ju0o3qk27c3s681dd.apps.googleusercontent.com';
var REDIRECT = 'http://localhost:61550/Test.html';
var LOGOUT = 'http://localhost:61550/Home.aspx';
var TYPE = 'token';
var _url = OAUTHURL + 'scope=' + SCOPE + '&client_id=' + CLIENTID + '&redirect_uri=' + REDIRECT + '&response_type=' + TYPE;
var acToken;
var tokenType;
var expiresIn;
var user;
var ids = ["githika.cs@gmail.com"];
function gLogin() {
    loggedIn = 1;
    $('#GoogleLogout').show();
    $('#GoogleLogin').hide();

    var win = window.open(_url, "windowname1", 'width=800, height=600');
    var pollTimer = window.setInterval(function () {
        try {
            console.log(win.document.URL);
            if (win.document.URL.indexOf(REDIRECT) != -1) {
                window.clearInterval(pollTimer);
                var url = win.document.URL;
                acToken = gup(url, 'access_token');
                tokenType = gup(url, 'token_type');
                expiresIn = gup(url, 'expires_in');

                win.close();
                debugger;
                validateToken(acToken);
            }
        }
        catch (e) {

        }
    }, 500);
}
function gup(url, name) {
    namename = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\#&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(url);
    if (results == null)
        return "";
    else
        return results[1];
}

function validateToken(token) {

    getUserInfo();
    $.ajax(

        {

            url: VALIDURL + token,
            data: null,
            success: function (responseText) {


            },

        });

}
var d;
function getUserInfo() {


    $.ajax({

        url: 'https://www.googleapis.com/oauth2/v1/userinfo?access_token=' + acToken,
        data: null,
        success: function (resp) {
            user = resp;
            $('#addmorebutton_link').show();

            $('#uname').html('Welcome ' + user.name);
            mail_id = user.email;
            var found = false;
            var admin = true;
            for (var i = 0; i < emails.length; i++) {
                if (emails[i].Email == user.email) {

                    found = true;
                    if (emails[i].Role=="user") {
                        admin = false;
                    }
                    else if (emails[i].Role == "admin")
                    {
                        admin = true;
                    }
                    break;
                }
            }

            if (found) {
                if (admin) {
                    $('#uemail').html('Admin Login: ' + user.email);
                    $('#ufullname').html(user.name);

                    $('#AddAdmins').show();
                    $('#AddAdminsResp').show();
                    $('#ViewRequests').show();
                    $('#ViewRequestsResp').show();

                    $('#editlink').show();
                    $('#deletelink').show();
                    adminLoggedIn = 1;
                }
                else if (user) {
                    $('#AddAdmins').hide();
                    $('#AddAdminsResp').hide();
                    $('#ViewRequestsResp').hide();

                    $('#ViewRequests').hide();
                    $('#ufullname').html(user.name);

                    $('#uemail').html('User Login: ' + user.email);
                    $('#editlink').hide();
                    $('#deletelink').hide();

                    adminLoggedIn = 0;
                }

            }
            else
            {
                $('#editlink').hide();
                $('#deletelink').hide();
                adminLoggedIn = 0;

                if (user.email != null) addUserEmail("user",user.email,user.name);
                $('#uemail').html('User Login: ' + user.email);

            }
            $('#imgHolder').attr('src', user.picture);
        },


    }),
    $.ajax({

        url: 'Home.aspx',

        type: 'POST',
        data: {
            email: user.email,
            name: user.name,
            gender: user.gender,
            lastname: user.lastname,
            location: user.location
        },
        success: function () {
            window.location.href = "Home.aspx";
        },
        async: "false",

        //dataType: "jsonp"

    });


}
function onSignIn(googleUser) {
    loggedIn = 1;

    $('#gSignIn').hide();

    var profile = googleUser.getBasicProfile();

    console.log('ID: ' + profile.getId()); // Do not send to your backend! Use an ID token instead.
    console.log('Name: ' + profile.getName());
    console.log('Image URL: ' + profile.getImageUrl());
    console.log('Email: ' + profile.getEmail()); // This is null if the 'email' scope is not present.
    console.log('token: ' + googleUser.getAuthResponse().id_token);
    $('#addmorebutton_link').show();

    $('#uname').html('Welcome ' + profile.getName());
    mail_id = profile.getEmail();
    var found = false;
    var admin = true;
    for (var i = 0; i < emails.length; i++) {
        if (emails[i].Email == profile.getEmail()) {

            found = true;
            if (emails[i].Role == "user") {
                admin = false;
            }
            else if (emails[i].Role == "admin") {
                admin = true;
            }
            break;
        }
    }

    if (found) {
        if (admin) {
            $('#uemail').html('Admin Login: ' + profile.getEmail());
            $('#ufullname').html(profile.getName());

            $('#AddAdmins').show();
            $('#AddAdminsResp').show();
            $('#ViewRequests').show();
            $('#ViewRequestsResp').show();

            $('#editlink').show();
            $('#deletelink').show();
            adminLoggedIn = 1;
        }
        else if (user) {
            $('#AddAdmins').hide();
            $('#AddAdminsResp').hide();
            $('#ViewRequestsResp').hide();

            $('#ViewRequests').hide();
            $('#ufullname').html(profile.getName());

            $('#uemail').html('User Login: ' + profile.getEmail());
            $('#editlink').hide();
            $('#deletelink').hide();

            adminLoggedIn = 0;
        }

    }
    else {
        $('#editlink').hide();
        $('#deletelink').hide();
        adminLoggedIn = 0;

        if (profile.getEmail() != null) addUserEmail("user", profile.getEmail(), profile.getName());
        $('#uemail').html('User Login: ' + profile.getEmail());

    }
    gapi.client.load('plus', 'v1', function () {
        var request = gapi.client.plus.people.get({
            'userId': 'me'
        });
        //Display the user details
        request.execute(function (resp) {
            var profileHTML = 'Welcome ' + resp.name.givenName + '! <a href="javascript:void(0);" onclick="signOut();">Sign out</a>';
            $('.userContent').html(profileHTML);

        });
    });
}
function signOut() {
    adminLoggedIn = 0;
    var auth2 = gapi.auth2.getAuthInstance();
    auth2.signOut().then(function () {
        console.log('User signed out.');
        $('#uemail').html('You are not logged in!');
        $('#addmorebutton_link').hide();
        $('#editlink').hide();
        $('#deletelink').hide();
        $('#gSignIn').show();
        $('.userContent').html("");
        $('#ufullname').html("");
    });
}
function gLogout()
{
    loggedIn = 0;

    $('#GoogleLogin').show();
    $('#GoogleLogout').hide();
    $('#addmorebutton_link').hide();
    $('#editlink').hide();
    $('#deletelink').hide();
    $('#uemail').html('You are not logged in!');

    window.location.href = "http://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=http://localhost:61550/Home.aspx";
}

function AddAdmins()
{
    $('.modal').show();
    document.getElementById("entered_ids").focus();

}

function closeP() {
    if (document.getElementById("entered_ids").value.length == 0)
        $('.modal').hide();
    else if (submitted == 0) {
        if (confirm('Close without adding?')) {
            document.getElementById("entered_ids").value = "";
            $('.modal').hide();
        } else {
            // Do nothing!
        }
    }
    else
    $('.modal').hide();
}
function closeR() {

    $('.modal_requests').hide();
    document.getElementById("jsonTab").style.display = "none";
    document.getElementById("selectCountry").selectedIndex = 0;
    document.getElementById("selectUser").selectedIndex = 0;

}
function submitIds() {
    submitted = 1;
    var role = "admin";
    var x = document.getElementById("entered_ids").value;
    if (x.length > 0) {
        var mArr = x.split(',');
        for (var i = 0; i < mArr.length; i++) {
            mArr[i] = mArr[i].trim();
        }
        var mArr_dis = mArr.filter(function (elem, index, self) {
            return index == self.indexOf(elem);
        });
        for (var i = 0; i < mArr_dis.length; i++) {
            if (valid_Id(mArr_dis[i])) {
                console.log(mArr_dis[i])

         
                var found = false;
                var admin = true;
                for (var j = 0; j < emails.length; j++) {
                    if (emails[j].Email == mArr_dis[i]) {

                        found = true;
                        if (emails[j].Role == "user") {
                            admin = false;
                        }
                        else if (emails[j].Role == "admin") {
                            admin = true;
                        }
                        break;
                    }
                }
                if (found) {
                    if (admin) {

                    }
                    else if (user) {
                        addUserEmail(role, mArr_dis[i],"");
                        $('.modal').hide();
                        document.getElementById("entered_ids").value = "";
                    }

                }
                else {
                    addUserEmail(role, mArr_dis[i],"");
                    document.getElementById("entered_ids").value = "";
                    $('.modal').hide();
                }
            }
            else {
                alert("Please enter a valid email address");
                document.getElementById("entered_ids").value = "";
                document.getElementById("entered_ids").focus();
            }
        }
    }
    else {
        alert('Please enter atleast one email address');
        document.getElementById("entered_ids").focus();

    }
}
function valid_Id(mail_id) {
    var x = mail_id;
    var atpos = x.indexOf("@");
    var dotpos = x.lastIndexOf(".");

    if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {
        return false;
    }
    else
        return true;
}
function contains(arr, element) {
    for (var i = 0; i < arr.length; i++) {
        if (arr[i] === element) {
            return true;
        }
    }
    return false;
}
function ViewRequests() {
    $('.modal_requests').show();


    
    document.getElementById('selectCountry').selectedIndex = 0;
    document.getElementById("selectUser").selectedIndex = 0;
    var countries = locationCentroids;
    var usrs = newlyAdded;


    var sel = document.getElementById('selectCountry');
    for (var i = 0; i < countries.Locations.length; i++) {
        var opt = document.createElement('option');
        opt.innerHTML = countries.Locations[i].Location;
        opt.value = countries.Locations[i].Location;
        sel.appendChild(opt);
    }
    var eids = [];
    for (var i = 0; i < usrs.length; i++) {
            eids.push(usrs[i].FullName);
        
        }
    var uids = [];
    for (var i = 0; i < usrs.length; i++) {
        uids.push(usrs[i].UID);
    }
    var unique = eids.filter(function (item, i, ar) { return ar.indexOf(item) === i; });

    var usel = document.getElementById('selectUser');
    for (var i = 0; i < unique.length; i++) {
        var opt = document.createElement('option');
        opt.innerHTML = unique[i];
        opt.value = unique[i];
        
            usel.appendChild(opt);
    }
    if (document.getElementById("selectUser").selectedIndex == 0 && document.getElementById("selectCountry").selectedIndex == 0)
        CreateTableFromJSON(uids, "Choose a country");


    $('#selectCountry').on('change', function () {
        var uid_arr1 = [];
        if ($("select#selectUser option").filter(":selected").text() == "Choose a User") {
            uid_arr1 = uids;
        }
        else {
            for (var i = 0; i < usrs.length; i++) {

                if (usrs[i].FullName == $("select#selectUser option").filter(":selected").text()) {
                    uid_arr1.push(usrs[i].UID);
                }
            }
        }
        CreateTableFromJSON(uid_arr1, this.value);
        highlight_row();
        // document.getElementById("jsonTab").style.display = "";
    })
   $('#selectUser').on('change', function () {
       var uid_arr2 = [];
       if ($("select#selectUser option").filter(":selected").text() == "Choose a User") {
           
           uid_arr2 = uids;
       }
       else {
           for (var i = 0; i < usrs.length; i++) {

               if (usrs[i].FullName == $("select#selectUser option").filter(":selected").text()) {
                   uid_arr2.push(usrs[i].UID);
               }
           }
       }

       CreateTableFromJSON(uid_arr2, $("select#selectCountry option").filter(":selected").text());
       highlight_row();
        //document.getElementById("jsonTab").style.display = "";
   })
   document.getElementById("jsonTab").style.display = "";

   highlight_row();
}
