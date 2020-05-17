using Serverside.Entities;
using Serverside.Models;
using Serverside.Services;
using Common.Helpers;
using GTANetworkAPI;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Serverside {
    public class Doors : Script {
        public Doors() {
            //var colShape = NAPI.ColShape.CreateCylinderColShape(new Vector3(981.7533f, -102.7987f, 74.8487f), 5f, 0);

            //colShape.SetData("Marker", 
            //    NAPI.Marker.CreateMarker(MarkerType.VerticalCylinder, colShape.Position, new Vector3(), new Vector3(), 1f, new Color(255, 255, 255, 255), false, 0)
            //);

            //colShape.SetData("Door", "v_ilev_lostdoor");

        }

        [ServerEvent(Event.PlayerEnterColshape)]
        public void EVENT_PlayerEnterColshape(ColShape colShape, Client player) {
            //if (colShape.HasData("Door")) {
            //    var doorType = (string)colShape.GetData("Door");

            //}
        }
    }
}
