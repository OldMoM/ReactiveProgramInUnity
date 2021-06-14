using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExampleProject
{
    public static class PlayerDataTransformTable
    {
        #region//PlayerBehaviorType访问快捷方式
        private static PlayerBehaviorType RUN => PlayerBehaviorType.RUN;
        private static PlayerBehaviorType SHOT => PlayerBehaviorType.SHOT;
        private static PlayerBehaviorType IDLE => PlayerBehaviorType.IDLE;
        private static PlayerBehaviorType DUCK => PlayerBehaviorType.DUCK;
        private static PlayerBehaviorType RUN_SHOT => PlayerBehaviorType.RUN_SHOT;
        private static PlayerBehaviorType JUMP => PlayerBehaviorType.JUMP;
        private static PlayerBehaviorType JUMP_SHOT => PlayerBehaviorType.JUMP_SHOT;
        private static PlayerBehaviorType GROUNDED => PlayerBehaviorType.GROUNDED;
        private static PlayerBehaviorType DUCK_SHOT => PlayerBehaviorType.DUCK_SHOT;
        private static PlayerBehaviorType RISE => PlayerBehaviorType.RISE;
        private static PlayerBehaviorType RISE_SHOT => PlayerBehaviorType.RISE_SHOT;
        #endregion

        /// <summary>
        /// 接收Event-行为转化表
        /// </summary>
        public static Dictionary<PlayerBehaviorType[], PlayerBehaviorType> eventBehaviorTransTab = new Dictionary<PlayerBehaviorType[], PlayerBehaviorType>()
        {
            { new PlayerBehaviorType[]{IDLE, IDLE, IDLE        }, IDLE      },
            { new PlayerBehaviorType[]{IDLE, SHOT, IDLE        }, SHOT      },
            { new PlayerBehaviorType[]{IDLE, IDLE, JUMP        }, JUMP      },
            { new PlayerBehaviorType[]{RUN , IDLE, IDLE        }, RUN       },
            { new PlayerBehaviorType[]{RUN, IDLE,  JUMP        }, JUMP      },
            { new PlayerBehaviorType[]{RUN , SHOT, IDLE        }, RUN_SHOT  },
            { new PlayerBehaviorType[]{RUN, SHOT, JUMP         }, JUMP_SHOT },
            { new PlayerBehaviorType[]{GROUNDED, GROUNDED, IDLE}, IDLE      },
            { new PlayerBehaviorType[]{DUCK, IDLE, IDLE        }, DUCK      },
            { new PlayerBehaviorType[]{DUCK, SHOT, IDLE        }, DUCK_SHOT },
            { new PlayerBehaviorType[]{DUCK, IDLE, JUMP        }, JUMP      },
            { new PlayerBehaviorType[]{DUCK, SHOT, JUMP        }, JUMP_SHOT },
            { new PlayerBehaviorType[]{RISE, IDLE, IDLE        }, RISE      },
            { new PlayerBehaviorType[]{RISE, SHOT, IDLE        }, RISE_SHOT },
            { new PlayerBehaviorType[]{RISE, IDLE, JUMP        }, JUMP      },
            { new PlayerBehaviorType[]{RISE, SHOT, JUMP        }, JUMP      },
        };
        /// <summary>
        /// Behavior-animation转化表
        /// </summary>
        public static Dictionary<PlayerBehaviorType, string> behaviorAnimationTransTab = new Dictionary<PlayerBehaviorType, string>()
        {
            {RUN      , "player_run"       },
            {IDLE     , "player_idle"      },
            {RUN_SHOT , "player_runShot"   },
            {SHOT     , "player_standShot" },
            {JUMP     , "player_jump"      },
            {JUMP_SHOT, "player_cling"     },
            {DUCK     , "player_duck"      },
            {DUCK_SHOT, "player_duck"      },
            {RISE     , "player_rise"      },
            {RISE_SHOT, "player_rise"      },
        };

        public static List<PlayerBehaviorType[]> illegalTranTranslation = new List<PlayerBehaviorType[]>()
        {
            {new PlayerBehaviorType[]{ JUMP_SHOT, JUMP} },
            {new PlayerBehaviorType[]{ JUMP     , DUCK} },
            {new PlayerBehaviorType[]{ JUMP_SHOT, DUCK} },
        };
    }    
}
