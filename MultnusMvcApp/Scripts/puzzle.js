$(document).ready(function () {
    try {
        
        var windowWidth = $(window).width();
        var windowHeight = $(window).height();

        var mainCircleX = windowWidth / 2 - 150;
        var mainCircleY = windowHeight / 2 - 150;

        var centerX = mainCircleX + 100;
        var centerY = mainCircleY + 100;

        var startPos = 1.5;
        var tweetCount = $("#HidTweetCircleCount").val();

        $(".main-circle").css("left", mainCircleX + 13);
        $(".main-circle").css("top", mainCircleY + 10);

        if (tweetCount % 2 == 0) {

            $("#childcircle1").css("left", centerX);
            $("#childcircle1").css("top", centerY - 220);

            var diaEndPos = (tweetCount / 2) + 1;

            $("#childcircle" + diaEndPos).css("left", centerX);
            $("#childcircle" + diaEndPos).css("top", centerY + 220);

            var remianPos = tweetCount - 2;
            var circlesPerHalfSection = remianPos / 2;
            var circularDistance = 1 / (circlesPerHalfSection + 1);
            circularDistance = circularDistance;
        }


        else {

            $("#childcircle1").css("left", centerX);
            $("#childcircle1").css("top", centerY - 220);

            var remianPos = tweetCount - 1;
            var circlesPerHalfSection = remianPos / 2;
            var circularDistance = 1 / (circlesPerHalfSection + 1);
            circularDistance = circularDistance + 0.02;
        }

        // Right Half Cirlce
        var CIRCULAR_DISTANCE = circularDistance;
        var diifQuadrant = false;
        for (var pos = 1; pos <= circlesPerHalfSection; ++pos) {

            var divIDPos = parseInt(pos + 1);

            if (startPos + circularDistance > 2.0) {

                diifQuadrant = true;
                startPos = startPos + circularDistance - 2;
            }


            if (diifQuadrant == true) {
                var pointX = centerX + Math.cos(parseFloat(startPos) * Math.PI) * 220;
                var pointY = centerY + Math.sin(parseFloat(startPos) * Math.PI) * 220;
                startPos = startPos + CIRCULAR_DISTANCE;

            }
            else {
                var pointX = centerX + Math.cos(parseFloat(startPos + circularDistance) * Math.PI) * 220;
                var pointY = centerY + Math.sin(parseFloat(startPos + circularDistance) * Math.PI) * 220;
                circularDistance = circularDistance + CIRCULAR_DISTANCE;
            }

            $("#childcircle" + divIDPos).css("left", pointX);
            $("#childcircle" + divIDPos).css("top", pointY);


        }

        // Left Half Cirlce
        circularDistance = CIRCULAR_DISTANCE;
        startPos = 1.5;
        diifQuadrant = false;
        var diffQuadPos;
        for (var pos = tweetCount; pos > tweetCount - circlesPerHalfSection; --pos) {

            var divIDPos = parseInt(pos);
            if (startPos - circularDistance < 1.0) {

                diifQuadrant = true;
                diffQuadPos = startPos - circularDistance;

            }

            if (diifQuadrant == true) {
                var pointX = centerX + Math.cos(parseFloat(diffQuadPos) * Math.PI) * 220;
                var pointY = centerY + Math.sin(parseFloat(diffQuadPos) * Math.PI) * 220;
                circularDistance = circularDistance + CIRCULAR_DISTANCE;

            }
            else {

                var pointX = centerX + Math.cos(parseFloat(startPos - circularDistance) * Math.PI) * 220;
                var pointY = centerY + Math.sin(parseFloat(startPos - circularDistance) * Math.PI) * 220;
                circularDistance = circularDistance + CIRCULAR_DISTANCE;
            }
            $("#childcircle" + divIDPos).css("left", pointX);
            $("#childcircle" + divIDPos).css("top", pointY);


        }


    } catch (err) {
        alert(err);
    }
});
