$(document).ready(function () {
    var board = $("#board");
    var message = $("#message");
    var manager = $.connection.blockMove;
    var me;

    $.connection.hub.start(function () {
        manager.initialize(600, 400).done(function (players) {
            $.each(players, function () {
                drawPlayer(this);
            });
        });
    });

    $("#join-btn").click(function () {
        var color = $("#join-color").val();
        if (color == "") {
            alert("Ange färg!");
            return;
        }
        manager.join(color).done(function (p) {
            if (!p) {
                alert("Det gick inte att joina... Det kanske redan finns en spelare med den färgen?");
                return;
            }

            me = p.Id;

            $("body").keypress(function (e) {
                //up
                if (e.keyCode == 38 || e.which == 119)
                    manager.move(me, "up");

                //right
                if (e.keyCode == 39 || e.which == 100)
                    manager.move(me, "right");

                //down
                if (e.keyCode == 40 || e.which == 115)
                    manager.move(me, "down");

                //left
                if (e.keyCode == 37 || e.which == 97)
                    manager.move(me, "left");
            });
        });
    });

    $("#join-color").focus(function () {
        $("#join-color").val("");
    });

    $("#reset-btn").click(function () {
        manager.reset();
        Reset();
    });

    manager.movePlayer = function (player) {
        drawPlayer(player);
    };
    manager.playerJoin = function (player) {
        showMessage("<div style=\"width:10px;height:10px;display:inline-block;background-color:" + player.Color + ";\"></div> has joined");
        drawPlayer(player);
    };
    manager.updatePointBlip = function (pointBlip) {
        $("#pointblip").remove();
        $("<div id=\"pointblip\" style=\"top:" + pointBlip.Cords.Y + ";left:" + pointBlip.Cords.X + ";width:" + pointBlip.Size + "px;height:" + pointBlip.Size + "px;\"></div>").appendTo(board);
    };

    function drawPlayer(player) {
        if (!player)
            return;

        var p = $(".player#" + player.Id);

        if (p.length > 0) {
            p.css("top", player.Cords.Y);
            p.css("left", player.Cords.X);
        } else {
            $("<div id=\"" + player.Id + "\" class=\"player\" style=\"background-color:" + player.Color + ";top:" + player.Cords.Y + ";left:" + player.Cords.X + "; width:" + player.Size + "px; height:" + player.Size + "px;\"></div>").appendTo(board);
        }

        var ps = $("#score-" + player.Id);

        if (ps.length > 0) {
            ps.find(".score").text(player.Score);
        } else {
            $("<li class=\"playerscore\" id=\"score-" + player.Id + "\"><div class=\"playerscoredisplay\" style=\" width:" + player.Size + "px; height:" + player.Size + "px; background-color: " + player.Color + ";\"></div>: <span class=\"score\">" + player.Score + "</span></li>").appendTo($("#scoreboard ul"));
        }
    }

    function showMessage(msg) {
        message.html(msg);
        message.fadeIn("slow", function () { message.fadeOut(3000, function () { message.html(""); }); });
    }

    function Reset() {
        $(".player").remove();
        $(".playerscore").remove();
    }
});