using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lf2datConverter.dat.Convenient;
using Microsoft.Xna.Framework;

namespace Lf2datConverter
{
    static class ConvenientConverter
    {
        public static Character ConvertToConvenientCharacter(dat.Base.Character character)
        {
            var convCharacter = new Character
            {
                Name = character.Name,
                Head = character.Head,
                Small = character.Small,
                SpriteFiles = character.SpriteFiles.Select(ConvertSpriteFile).ToList(),
                WalkingFrameRate = character.WalkingFrameRate,
                WalkingSpeed = new Vector3(character.WalkingSpeed, 0, character.WalkingSpeedZ),
                RunningFrameRate = character.RunningFrameRate,
                RunningSpeed = new Vector3(character.RunningSpeed, 0, character.RunningSpeedZ),
                HeavyWalkingSpeed = new Vector3(character.HeavyWalkingSpeed, 0, character.HeavyWalkingSpeedZ),
                HeavyRunningSpeed = new Vector3(character.HeavyRunningSpeed, 0, character.HeavyRunningSpeedZ),
                JumpHeight = character.JumpHeight,
                JumpDistance = new Vector3(character.JumpDistance, 0, character.JumpDistanceZ),
                DashHeight = character.DashHeight,
                DashDistance = new Vector3(character.DashDistance, 0, character.DashDistanceZ),
                RowingHeight = character.RowingHeight,
                RowingDistance = character.RowingDistance,
                Frames = character.Frames.Select(ConvertCharacterFrame).ToList()
            };

            return convCharacter;
        }

        private static SpriteFile ConvertSpriteFile(dat.Base.SpriteFile file)
        {
            var convFile = new SpriteFile
            {
                Columns = file.Col,
                FinishID = file.FinishID,
                Height = file.H,
                Rows = file.Row,
                Sprite = file.Sprite,
                StartID = file.StartID,
                Width = file.W
            };
            return convFile;
        }

        private static CharacterFrame ConvertCharacterFrame(dat.Base.CharacterFrame frame)
        {
            var convFrame = new CharacterFrame
            {
                FrameNumber = frame.FrameNumber,
                Name = frame.Name,
                Pic = frame.Pic,
                State = frame.State,
                Wait = frame.Wait,
                Next = frame.Next,
                DV = new Vector3(frame.DVX, frame.DVY, frame.DVZ),
                Center = new Vector2(frame.CenterX, frame.CenterY),
                HitA = frame.HitA,
                HitD = frame.HitD,
                HitJ = frame.HitJ,
                HitDA = frame.HitDA,
                HitDJ = frame.HitDJ,
                HitFA = frame.HitFA,
                HitFJ = frame.HitFJ,
                HitJA = frame.HitJA,
                HitUA = frame.HitUA,
                HitUJ = frame.HitUJ,
                MP = frame.MP,
                Sound = frame.Sound,
                FrameElements = frame.FrameElements.Select(ConvertFrameElement).ToList()
            };
            return convFrame;
        }

        private static FrameElement ConvertFrameElement(dat.Base.FrameElement frameElement)
        {
            FrameElement convFrameElement = null;
            switch (frameElement.Name)
            {
                case "WeaponPoint":
                {
                    var weaponPoint = frameElement as dat.Base.WeaponPoint;
                    if (weaponPoint != null)
                        convFrameElement = new WeaponPoint
                        {
                            Kind = weaponPoint.Kind,
                            Attacking = weaponPoint.Attacking,
                            Cover = weaponPoint.Cover,
                            DVX = weaponPoint.DVX,
                            DVY = weaponPoint.DVY,
                            DVZ = weaponPoint.DVZ,
                            WeaponAct = weaponPoint.WeaponAct,
                            X = weaponPoint.X,
                            Y = weaponPoint.Y
                        };
                    break;
                }
                case "ObjectPoint":
                {
                    var objectPoint = frameElement as dat.Base.ObjectPoint;
                    if (objectPoint != null)
                    {
                        convFrameElement = new ObjectPoint
                        {
                            Action = objectPoint.Action,
                            DVX = objectPoint.DVX,
                            DVY = objectPoint.DVY,
                            Facing = objectPoint.Facing,
                            Kind = objectPoint.Kind,
                            OID = objectPoint.OID,
                            X = objectPoint.X,
                            Y = objectPoint.Y
                        };
                    }
                    break;
                }
                    case "CatchingPoint":
                {
                    var catchingPoint = frameElement as dat.Base.CatchingPoint;
                    if (catchingPoint != null)
                    {
                        convFrameElement = new CatchingPoint
                        {
                            AAction = catchingPoint.AAction,
                            Cover = catchingPoint.Cover,
                            Decrease = catchingPoint.Decrease,
                            DirControl = catchingPoint.DirControl,
                            Hurtable = catchingPoint.Hurtable,
                            Injury = catchingPoint.Injury,
                            JAction = catchingPoint.JAction,
                            TAction = catchingPoint.TAction,
                            ThrowInjury = catchingPoint.ThrowInjury,
                            ThrowVX = catchingPoint.ThrowVX,
                            ThrowVY = catchingPoint.ThrowVY,
                            ThrowVZ = catchingPoint.ThrowVZ,
                            VAction = catchingPoint.VAction,
                            X = catchingPoint.X,
                            Y = catchingPoint.Y
                        };
                    }
                    break;
                }
                    case "CaughtPoint":
                {
                    var caughtPoint = frameElement as dat.Base.CaughtPoint;
                    if (caughtPoint != null)
                    {
                        convFrameElement = new CaughtPoint
                        {
                            BackHurtAct = caughtPoint.BackHurtAct,
                            FrontHurtAct = caughtPoint.FrontHurtAct,
                            X = caughtPoint.X,
                            Y = caughtPoint.Y
                        };
                    }
                    break;
                }
                    case "BloodPoint":
                {
                    var bloodPoint = frameElement as dat.Base.BloodPoint;
                    if (bloodPoint != null)
                    {
                        convFrameElement = new BloodPoint
                        {
                            X = bloodPoint.X,
                            Y = bloodPoint.Y
                        };
                    }
                    break;
                }
                    case "Body":
                {
                    var body = frameElement as dat.Base.Body;
                    if (body != null)
                    {
                        convFrameElement = new Body
                        {
                            H = body.H,
                            Kind = body.Kind,
                            W = body.W,
                            X = body.X,
                            Y = body.Y
                        };
                    }
                    break;
                }
                    case "NormalHit":
                {
                    var normalHit = frameElement as dat.Base.NormalHit;
                    if (normalHit != null)
                    {
                        convFrameElement = new NormalHit
                        {
                            ARest = normalHit.ARest,
                            BDefend = normalHit.BDefend,
                            DVX = normalHit.DVX,
                            DVY = normalHit.DVY,
                            Effect = normalHit.Effect,
                            Fall = normalHit.Fall,
                            H = normalHit.H,
                            Injury = normalHit.Injury,
                            VRest = normalHit.VRest,
                            ZWidth = normalHit.ZWidth,
                            X = normalHit.X,
                            Y = normalHit.Y,
                            W = normalHit.W
                        };
                    }
                    break;
                }
                    case "CatchDoP":
                {
                    var catchDoP = frameElement as dat.Base.CatchDoP;
                    if (catchDoP != null)
                    {
                        convFrameElement = new CatchDoP
                        {
                            CatchingActBack = catchDoP.CatchingActBack,
                            CatchingActFront = catchDoP.CatchingActFront,
                            CaughtActBack = catchDoP.CaughtActBack,
                            CaughtActFront = catchDoP.CaughtActFront,
                            H = catchDoP.H,
                            W = catchDoP.W,
                            X = catchDoP.X,
                            Y = catchDoP.Y
                        };
                    }
                    break;
                }
                    case "PickWeapon":
                {
                    var pickWeapon = frameElement as dat.Base.PickWeapon;
                    if (pickWeapon != null)
                    {
                        convFrameElement = new PickWeapon
                        {
                            H = pickWeapon.H,
                            VRest = pickWeapon.VRest,
                            W = pickWeapon.W,
                            X = pickWeapon.X,
                            Y = pickWeapon.Y,
                        };
                    }
                    break;
                }
                    case "CatchBody":
                {
                    var catchBody = frameElement as dat.Base.CatchBody;
                    if (catchBody != null)
                    {
                        convFrameElement = new CatchBody
                        {
                            CatchingActBack = catchBody.CatchingActBack,
                            CatchingActFront = catchBody.CatchingActFront,
                            CaughtActBack = catchBody.CaughtActBack,
                            CaughtActFront = catchBody.CaughtActFront,
                            H = catchBody.H,
                            W = catchBody.W,
                            X = catchBody.X,
                            Y = catchBody.Y
                        };
                    }
                    break;
                }
                    case "Falling":
                {
                    var falling = frameElement as dat.Base.Falling;
                    if (falling != null)
                    {
                        convFrameElement = new Falling
                        {
                            BDefend = falling.BDefend,
                            DVX = falling.DVX,
                            Fall = falling.Fall,
                            H = falling.H,
                            Injury = falling.Injury,
                            VRest = falling.VRest,
                            X = falling.X,
                            Y = falling.Y,
                            W = falling.W
                        };
                    }
                    break;
                }
                    case "WeaponStrength":
                {
                    var weaponStrength = frameElement as dat.Base.WeaponStrength;
                    if (weaponStrength != null)
                    {
                        convFrameElement = new WeaponStrength
                        {
                            BDefend = weaponStrength.BDefend,
                            DVX = weaponStrength.DVX,
                            Fall = weaponStrength.Fall,
                            H = weaponStrength.H,
                            Injury = weaponStrength.Injury,
                            X = weaponStrength.X,
                            Y = weaponStrength.Y,
                            W = weaponStrength.W
                        };
                    }
                    break;
                }
                    case "SuperPunch":
                {
                    var superPunch = frameElement as dat.Base.SuperPunch;
                    if (superPunch != null)
                    {
                        convFrameElement = new SuperPunch
                        {
                            H = superPunch.H,
                            VRest = superPunch.VRest,
                            X = superPunch.X,
                            Y = superPunch.Y,
                            W = superPunch.W
                        };
                    }
                    break;
                }
                    case "PickWeapon2":
                {
                    var pickWeapon2 = frameElement as dat.Base.PickWeapon2;
                    if (pickWeapon2 != null)
                    {
                        convFrameElement = new PickWeapon2
                        {
                            H = pickWeapon2.H,
                            VRest = pickWeapon2.VRest,
                            X = pickWeapon2.X,
                            Y = pickWeapon2.Y,
                            W = pickWeapon2.W
                        };
                    }
                    break;
                }
                    case "HealBall":
                {
                    var healBall = frameElement as dat.Base.HealBall;
                    if (healBall != null)
                    {
                        convFrameElement = new HealBall
                        {
                            DVX = healBall.DVX,
                            H = healBall.H,
                            Injury = healBall.Injury,
                            X = healBall.X,
                            Y = healBall.Y,
                            W = healBall.W
                        };
                    }
                    break;
                }
                    case "ReflectiveShield":
                {
                    var reflectiveShield = frameElement as dat.Base.ReflectiveShield;
                    if (reflectiveShield != null)
                    {
                        convFrameElement = new ReflectiveShield
                        {
                            DVX = reflectiveShield.DVX,
                            Fall = reflectiveShield.Fall,
                            H = reflectiveShield.H,
                            Injury = reflectiveShield.Injury,
                            VRest = reflectiveShield.VRest,
                            X = reflectiveShield.X,
                            Y = reflectiveShield.Y,
                            W = reflectiveShield.W
                        };
                    }
                    break;
                }
                    case "SonataOfDeath":
                {
                    var sonataOfDeath = frameElement as dat.Base.SonataOfDeath;
                    if (sonataOfDeath != null)
                    {
                        convFrameElement = new SonataOfDeath
                        {
                            H = sonataOfDeath.H,
                            Injury = sonataOfDeath.Injury,
                            VRest = sonataOfDeath.VRest,
                            ZWidth = sonataOfDeath.ZWidth,
                            X = sonataOfDeath.X,
                            Y = sonataOfDeath.Y,
                            W = sonataOfDeath.W
                        };
                    }
                    break;
                }
                    case "SonataOfDeath2":
                {
                    var sonataOfDeath2 = frameElement as dat.Base.SonataOfDeath2;
                    if (sonataOfDeath2 != null)
                    {
                        convFrameElement = new SonataOfDeath2
                        {
                            H = sonataOfDeath2.H,
                            Injury = sonataOfDeath2.Injury,
                            VRest = sonataOfDeath2.VRest,
                            ZWidth = sonataOfDeath2.ZWidth,
                            X = sonataOfDeath2.X,
                            Y = sonataOfDeath2.Y,
                            W = sonataOfDeath2.W
                        };
                    }
                    break;
                }
                    case "SolidObject":
                {
                    var solidObject = frameElement as dat.Base.SolidObject;
                    if (solidObject != null)
                    {
                        convFrameElement = new SolidObject
                        {
                            H = solidObject.H,
                            VRest = solidObject.VRest,
                            X = solidObject.X,
                            Y = solidObject.Y,
                            W = solidObject.W
                        };
                    }
                    break;
                }
                    case "WindWhirlWind":
                {
                    var windWhirlWind = frameElement as dat.Base.WindWhirlWind;
                    if (windWhirlWind != null)
                    {
                        convFrameElement = new WindWhirlWind
                        {
                            BDefend = windWhirlWind.BDefend,
                            DVX = windWhirlWind.DVX,
                            DVY = windWhirlWind.DVY,
                            Fall = windWhirlWind.Fall,
                            H = windWhirlWind.H,
                            Injury = windWhirlWind.Injury,
                            VRest = windWhirlWind.VRest,
                            ZWidth = windWhirlWind.ZWidth,
                            X = windWhirlWind.X,
                            Y = windWhirlWind.Y,
                            W = windWhirlWind.W
                        };
                    }
                    break;
                }
                    case "FrostWhirlWind":
                {
                    var frostWhirlWind = frameElement as dat.Base.FrostWhirlWind;
                    if (frostWhirlWind != null)
                    {
                        convFrameElement = new FrostWhirlWind
                        {
                            BDefend = frostWhirlWind.BDefend,
                            DVX = frostWhirlWind.DVX,
                            DVY = frostWhirlWind.DVY,
                            Fall = frostWhirlWind.Fall,
                            H = frostWhirlWind.H,
                            Injury = frostWhirlWind.Injury,
                            VRest = frostWhirlWind.VRest,
                            ZWidth = frostWhirlWind.ZWidth,
                            X = frostWhirlWind.X,
                            Y = frostWhirlWind.Y,
                            W = frostWhirlWind.W
                        };
                    }
                    break;
                }
            }
            return convFrameElement;
        }
    }
}