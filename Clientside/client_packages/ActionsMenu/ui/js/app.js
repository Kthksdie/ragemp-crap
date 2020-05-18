//https://stackoverflow.com/questions/21912226/how-can-i-find-current-element-on-mouseover-using-jquery

let actionItems = null;

function BuildActions() {
    if (actionItems !== null) {
        return actionItems;
    }

    var mainActions = {
        0: {
            children: [100, 200, 300]
        },

        100: {
            label: "<b>Inventory</b>",
            parent: 0,
            action: () => {
                console.log('Inventory');
            },
        },

        200: {
            label: "<b>Walking Styles</b>",
            parent: 0,
            children: [201, 202, 203, 204, 205, 206, 207]
        },

        300: {
            label: "<b>Emotes</b>",
            parent: 0,
            children: [301, 302, 303, 304, 305, 306, 307]
        },
    };

    var walkingStyles_Page1 = {
        201: {
            label: "<b>Normal</b>",
            parent: 200,
            action: () => {
                mp.trigger('applyWalkingStyle', 0);
            },
        },

        202: {
            label: "<b>Brave</b>",
            parent: 200,
            action: () => {
                mp.trigger('applyWalkingStyle', 1);
            },
        },

        203: {
            label: "<b>Confident</b>",
            parent: 200,
            action: () => {
                mp.trigger('applyWalkingStyle', 2);
            },
        },

        204: {
            label: "<b>Drunk</b>",
            parent: 200,
            action: () => {
                mp.trigger('applyWalkingStyle', 3);
            },
        },

        205: {
            label: "<b>Fat</b>",
            parent: 200,
            action: () => {
                mp.trigger('applyWalkingStyle', 4);
            },
        },

        206: {
            label: "<b>Gangster</b>",
            parent: 200,
            action: () => {
                mp.trigger('applyWalkingStyle', 5);
            },
        },

        207: {
            label: "<span><b>Walking Styles</b><br />Page 2</span>",
            parent: 200,
            children: [208, 209, 210, 211, 212, 213]
        },
    };

    var walkingStyles_Page2 = {
        208: {
            label: "<b>Hurry</b>",
            parent: 200,
            action: () => {
                mp.trigger('applyWalkingStyle', 6);
            },
        },

        209: {
            label: "<b>Injured</b>",
            parent: 200,
            action: () => {
                mp.trigger('applyWalkingStyle', 7);
            },
        },

        210: {
            label: "<b>Intimidated</b>",
            parent: 200,
            action: () => {
                mp.trigger('applyWalkingStyle', 8);
            },
        },

        211: {
            label: "<b>Quick</b>",
            parent: 200,
            action: () => {
                mp.trigger('applyWalkingStyle', 9);
            },
        },

        212: {
            label: "<b>Sad</b>",
            parent: 200,
            action: () => {
                mp.trigger('applyWalkingStyle', 10);
            },
        },

        213: {
            label: "<b>Tough</b>",
            parent: 200,
            action: () => {
                mp.trigger('applyWalkingStyle', 11);
            },
        },
    };

    for (attr in walkingStyles_Page1) {
        mainActions[attr] = walkingStyles_Page1[attr];
    }

    for (attr in walkingStyles_Page2) {
        mainActions[attr] = walkingStyles_Page2[attr];
    }

    var emotes_Page1 = {
        301: {
            label: "<b>Emote 1</b>",
            parent: 300,
            action: () => {
                console.log('Emote 1');
            },
        },

        302: {
            label: "<b>Emote 2</b>",
            parent: 300,
            action: () => {
                console.log('Emote 2');
            },
        },

        303: {
            label: "<b>Emote 3</b>",
            parent: 300,
            action: () => {
                console.log('Emote 3');
            },
        },

        304: {
            label: "<b>Emote 4</b>",
            parent: 300,
            action: () => {
                console.log('Emote 4');
            },
        },

        305: {
            label: "<b>Emote 5</b>",
            parent: 300,
            action: () => {
                console.log('Emote 5');
            },
        },

        306: {
            label: "<b>Emote 6</b>",
            parent: 300,
            action: () => {
                console.log('Emote 6');
            },
        },

        307: {
            label: "<span><b>Emotes</b><br />Page 2</span>",
            parent: 300,
            children: [308, 309, 310, 311, 312, 313, 314]
        },
    };

    var emotes_Page2 = {
        308: {
            label: "<b>Emote 7</b>",
            parent: 307,
            action: () => {
                console.log('Emote 7');
            },
        },

        309: {
            label: "<b>Emote 8</b>",
            parent: 307,
            action: () => {
                console.log('Emote 8');
            },
        },

        310: {
            label: "<b>Emote 9</b>",
            parent: 307,
            action: () => {
                console.log('Emote 9');
            },
        },

        311: {
            label: "<b>Emote 10</b>",
            parent: 307,
            action: () => {
                console.log('Emote 10');
            },
        },

        312: {
            label: "<b>Emote 11</b>",
            parent: 307,
            action: () => {
                console.log('Emote 11');
            },
        },

        313: {
            label: "<b>Emote 12</b>",
            parent: 307,
            action: () => {
                console.log('Emote 12');
            },
        },

        314: {
            label: "<span><b>Emotes</b><br />Page 3</span>",
            parent: 307,
            children: [315, 316, 317, 318, 319, 320]
        },
    };

    var emotes_Page3 = {
        315: {
            label: "<b>Emote 13</b>",
            parent: 314,
            action: () => {
                console.log('Emote 13');
            },
        },

        316: {
            label: "<b>Emote 14</b>",
            parent: 314,
            action: () => {
                console.log('Emote 14');
            },
        },

        317: {
            label: "<b>Emote 15</b>",
            parent: 314,
            action: () => {
                console.log('Emote 15');
            },
        },

        318: {
            label: "<b>Emote 16</b>",
            parent: 314,
            action: () => {
                console.log('Emote 16');
            },
        },

        319: {
            label: "<b>Emote 17</b>",
            parent: 314,
            action: () => {
                console.log('Emote 17');
            },
        },

        320: {
            label: "<b>Emote 18</b>",
            parent: 314,
            action: () => {
                console.log('Emote 18');
            },
        },
    };

    for (attr in emotes_Page1) {
        mainActions[attr] = emotes_Page1[attr];
    }

    for (attr in emotes_Page2) {
        mainActions[attr] = emotes_Page2[attr];
    }

    for (attr in emotes_Page3) {
        mainActions[attr] = emotes_Page3[attr];
    }

    return mainActions;
}

(function () {
    var ActionWheel = function () {
        var data = BuildActions();

        var $wheel = $('#ActionWheel'),
            that = this;

        // Not needed?
        //$wheel.empty();
        //$wheel.attr("data-wheel-id", 0);

        this.data = data;
        this.id = 0;

        this.update = function (id) {

            this.id = id;

            var currentNode = this.data[this.id],
                currentChildren = currentNode.children,
                hasParent = (currentNode.parent !== undefined);

            // Clear the wheel
            $wheel.empty();

            // Children nodes
            if (currentChildren) {

                var nChildren = currentChildren.length;
                var angles = [];

                switch (nChildren) {
                    case 1:
                        angles = [0];
                        break;
                    case 2:
                        angles = [7 * Math.PI / 4, Math.PI / 4];
                        break;
                    case 3:
                        angles = [3 * Math.PI / 2, 0, Math.PI / 2];
                        break;
                    case 4:
                        angles = [Math.PI / 4, 3 * Math.PI / 4, 5 * Math.PI / 4, 7 * Math.PI / 4];
                        break;
                    case 5:
                        angles = [Math.PI / 4, 3 * Math.PI / 4, 0, 5 * Math.PI / 4, 7 * Math.PI / 4];
                        break;
                    case 6:
                        angles = [Math.PI / 4, Math.PI / 2, 3 * Math.PI / 4, 0, 5 * Math.PI / 4, 7 * Math.PI / 4];
                        break;
                    case 7:
                        angles = [0, Math.PI / 4, Math.PI / 2, 3 * Math.PI / 4, 5 * Math.PI / 4, 3 * Math.PI / 2, 7 * Math.PI / 4];
                        break;
                    case 8:
                        angles = [5 * Math.PI / 4, 3 * Math.PI / 2, 7 * Math.PI / 4, 0, Math.PI / 4, Math.PI / 2, 3 * Math.PI / 4, Math.PI];
                        break;
                    default:
                        break;
                }

                for (var i = 0; i < nChildren; i++) {
                    var childId = currentChildren[i];
                    var child = data[childId];

                    //var wheelSize = 460;	// TODO : get wheel size
                    //var nodeSize = 125;	  // TODO : get node size
                    //var radius = (wheelSize - nodeSize) / 2;	// TODO

                    var $node = $('<div class="node node--child node--' + i + '" data-wheel-id="' + childId + '"><span>' + child.label + '</span></div>');

                    var angle = angles[i];
                    var radius = 50 - 12.5; // In %
                    var posX = 50;
                    var posY = 50;

                    $node.css({
                        left: posX + "%",
                        top: posY + "%"
                    });

                    var $nodeBar = $('<div class="node-bar"></div>')
                    $nodeBar.css({
                        transform: "rotate(" + (angles[i] + Math.PI / 2) + "rad)"
                    });

                    $node.append($nodeBar);
                    $wheel.append($node);

                    posX = 50 + Math.cos(angle - Math.PI / 2) * radius;
                    posY = 50 + Math.sin(angle - Math.PI / 2) * radius;

                    $node.delay(25 * i).animate(
                        {
                            left: posX + "%",
                            top: posY + "%"
                        },
                        {
                            duration: 100,
                            easing: "swing"
                        }
                    );
                }
            }

            // Current node
            if (currentNode.label !== undefined) {
                $wheel.append('<div class="node node--current"><span>' + currentNode.label + '</span></div>');
            }
            else {
                $wheel.append('<div class="node node--current"><span><b>Actions</b></span></div>');
            }

            // Parent (back)
            if (hasParent) {
                $wheel.append('<div class="node node--parent" data-wheel-id="' + currentNode.parent + '"><span>BACK</span><div class="node-bar"></div></div>');
            }

            // EVENTS

            $(".node--child").on("click", function (e) {
                var id = $(this).attr("data-wheel-id");

                if (that.data[id].children !== undefined) {
                    // Fade out things
                    $(".node--child").each(function (i) {
                        $(this).stop().delay(25 * i).animate(
                            { opacity: 0 },
                            {
                                duration: 100,
                                complete: function () {
                                    // Navigate to the child ID
                                    that.update(id);	// TODO : multiple !!!
                                }
                            }
                        );
                    });

                }
                else if (that.data[id].action !== undefined) {
                    that.data[id].action();
                }
            });

            $(".node--parent").on("click", function (e) {
                // Navigate to the parent ID
                var id = $(this).attr("data-wheel-id");
                that.update(id);
            });
        }

        if ($wheel.attr("data-wheel-id")) {
            this.id = $wheel.attr("data-wheel-id");
        }

        this.update(this.id);
    }

    $(document).ready(function () {
        var wheel = new ActionWheel();
    });
})();

var testData = [
    {
        Name: 'Inventory',
        Action: () => {
            console.log('Inventory');
        },
    },
    {
        Name: 'Walking Styles',
        Children: [
            {
                Name: 'Normal',
                Action: () => {
                    mp.trigger('applyWalkingStyle', 0);
                },
            },
        ]
    },
    {
        Name: 'Emotes',
        Children: [

        ]
    },
];