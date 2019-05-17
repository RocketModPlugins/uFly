using Rocket.Unturned.Player;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;

namespace uFly
{
    public class uFly : Plugin
    {
        public List<UnturnedPlayer> playersFlying = new List<UnturnedPlayer>();

        protected override async Task OnActivate(bool isFromReload)
        {
            Logger.LogInformation("[uFly] Activated");
        }

        public void UnturnedPlayerEvents_OnPlayerUpdateGesture(UnturnedPlayer player, UnturnedPlayerEvents.PlayerGesture gesture)
        {
            if (gesture == UnturnedPlayerEvents.PlayerGesture.Point)
            {
                playersFlying.Add(player);
                player.Player.movement.gravity = 0;
            }

            if (gesture == UnturnedPlayerEvents.PlayerGesture.Wave)
            {
                playersFlying.Remove(player);
                player.Player.movement.gravity = 1;
            }
        }

        public void FixedUpdate()
        {
            foreach (UnturnedPlayer player in playersFlying)
            {
                if (player.Player.input.keys[0])
                {
                    player.Teleport(new Vector3(player.Position.x, player.Position.y, player.Position.z), player.Rotation);
                }

                if (player.Player.input.keys[5])
                {
                    player.Teleport(new Vector3(player.Position.x, player.Position.y - 1, player.Position.z), player.Rotation);
                }
            }
        }

        protected override async Task OnDeactivate()
        {
            Logger.LogInformation("[uFly] Deactivating");
        }
    }
}
