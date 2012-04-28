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
        if (color == "")
            alert("Ange färg!")
        manager.join(color).done(function (p) {
            if (!p) {
                alert("Det gick inte att joina... Det kanske redan finns en spelare med den färgen?");
                return;
            }

            me = p.Id;

            drawPlayer(p);

            $("body").keypress(function (e) {
                //up
                if (e.keyCode == 38 || e.which == 119)
                    manager.move(me, "up").done(function (p) { if (p) drawPlayer(p); });

                //right
                if (e.keyCode == 39 || e.which == 100)
                    manager.move(me, "right").done(function (p) { if (p) drawPlayer(p); });

                //down
                if (e.keyCode == 40 || e.which == 115)
                    manager.move(me, "down").done(function (p) { if (p) drawPlayer(p); });

                //left
                if (e.keyCode == 37 || e.which == 97)
                    manager.move(me, "left").done(function (p) { if (p) drawPlayer(p); });
            });
        });
    });

    $("#join-color").focus(function () {
        $("#join-color").val("");
    });

    manager.movePlayer = function (player) {
        drawPlayer(player);
    };
    manager.playerJoin = function (player) {
        showMessage("<div style=\"width:10px;height:10px;display:inline-block;background-color:" + player.Color + ";\"></div> has joined");
        drawPlayer(player);
    };

    function drawPlayer(player) {
        if (!player)
            return;
        
        var p = $("#" + player.Id);

        if (p.length > 0) {
            p.css("top", player.Cords.Y);
            p.css("left", player.Cords.X);
            return;
        }

        $("<div id=\"" + player.Id + "\" class=\"player\" style=\"background-color:" + player.Color + ";top:" + player.YPosition + ";left:" + player.XPosition + "; width:"+player.Size+"; height:"+player.Size+";\"></div>").appendTo(board);
    }

    function showMessage(msg) {
        message.html(msg);
        message.fadeIn("slow", function () { message.fadeOut(3000, function () { message.html(""); }); });
    }
});