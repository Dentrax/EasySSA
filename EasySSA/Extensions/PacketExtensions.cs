using EasySilkroadSecurityApi.Core.Tweening;
using EasySilkroadSecurityApi.SSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySilkroadSecurityApi.Extensions {
    public static class PacketExtensions {
        public static Tweener DODebug(this Packet target, bool advanced = false) {
            return EasySSA.DOPacketDebug(() => target.position, x => target.position = x, endValue, duration).SetOptions(advanced).SetTarget(target);

        }
    }
}
