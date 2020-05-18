



let UI = null;
let state = 0;
let localPlayer = mp.players.local;

mp.keys.bind(90, true, _ => {
    if (state === 0) {
        showUI(true);
    }
    else if (state === 1) {
        showUI(false);
    }
});

mp.events.add('click', (x, y, upOrDown, leftOrRight, relativeX, relativeY, worldPosition, hitEntity) => {
    if (state === 1) {
        if (upOrDown == "down") {
            if (leftOrRight == "right") {
                showUI(false);
            }
        }
    }
});

function showUI(visible) {
    if (UI === null) {
        UI = mp.browsers.new('package://ActionsMenu/ui/index.html');
    }

    //let safeZone = mp.game.graphics.getSafeZoneSize();
    //let aspectRatio = mp.game.graphics.getScreenAspectRatio(false);
    //let resolution = mp.game.graphics.getScreenActiveResolution(0, 0);
    //let scaleX = 1.0 / resolution.x;
    //let scaleY = 1.0 / resolution.y;

    //mp.gui.cursor.position

    mp.gui.cursor.visible = visible;
    state = visible === true ? 1 : 0;

    if (visible === true) {
        // Reload seems to mess things up?
        //UI.execute("location.reload()");
        UI.execute("$('body').fadeIn('fast')");
    }
    else {
        UI.execute("$('body').fadeOut('fast')");
    }
}

mp.events.add("applyWalkingStyle", (index) => {
    mp.events.callRemote("setWalkingStyle", index);
});

mp.events.add("entityStreamIn", (entity) => {
    if (entity.type !== "player") return;
    setWalkingStyle(entity, entity.getVariable("walkingStyle"));
});

mp.events.addDataHandler("walkingStyle", (entity, value) => {
    if (entity.type === "player") setWalkingStyle(entity, value);
});

function setWalkingStyle(player, style) {
    if (!style) {
        player.resetMovementClipset(0.0);
    }
    else {
        if (!mp.game.streaming.hasClipSetLoaded(style)) {
            mp.game.streaming.requestClipSet(style);
            while (!mp.game.streaming.hasClipSetLoaded(style)) mp.game.wait(0);
        }

        player.setMovementClipset(style, 0.0);
    }
}