using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LF2datConverter.dat.Convenient;
using Microsoft.Xna.Framework;

namespace LF2datConverter
{
    static class ConvenientConverter
    {
        public static Character ConvertToConvenientCharacter(dat.Base.Character character)
        {
            var convCharacter = new Character
            {
                Name = character.Name,
                HeadPicture = character.Head,
                SmallPicture = character.Small,
                SpriteFiles = character.SpriteFiles.Select(ConvertSpriteFile).ToList(),
                WalkingFrameRate = character.WalkingFrameRate,
                WalkingSpeed = new Vector3(character.WalkingSpeed, 0, character.WalkingSpeedZ),
                RunningFrameRate = character.RunningFrameRate,
                RunningSpeed = new Vector3(character.RunningSpeed, 0, character.RunningSpeedZ),
                HeavyWalkingSpeed = new Vector3(character.HeavyWalkingSpeed, 0, character.HeavyWalkingSpeedZ),
                HeavyRunningSpeed = new Vector3(character.HeavyRunningSpeed, 0, character.HeavyRunningSpeedZ),
                JumpDistance = new Vector3(character.JumpDistance, character.JumpHeight, character.JumpDistanceZ),
                DashDistance = new Vector3(character.DashDistance, character.DashHeight, character.DashDistanceZ),
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
                            WeaponAct = weaponPoint.WeaponAct,
                            Position = new Point(weaponPoint.X, weaponPoint.Y),
                            VelocityDelta = new Vector3(weaponPoint.DVX, weaponPoint.DVY, weaponPoint.DVZ)
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
                            Facing = objectPoint.Facing,
                            Kind = objectPoint.Kind,
                            ObjectID = objectPoint.OID,
                            Position = new Point(objectPoint.X, objectPoint.Y),
                            VelocityDelta = new Vector3(objectPoint.DVX, objectPoint.DVY, 0)
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
                            VAction = catchingPoint.VAction,
                            Position = new Point(catchingPoint.X, catchingPoint.Y),
                            VelocityDelta = new Vector3(catchingPoint.ThrowVX, catchingPoint.ThrowVY, catchingPoint.ThrowVZ)
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
                            Position = new Point(caughtPoint.X, caughtPoint.Y)
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
                            Position = new Point(bloodPoint.X, bloodPoint.Y)
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
                            Area = new Rectangle(body.X, body.Y, body.W, body.H),
                            Kind = body.Kind
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
                            Area = new Rectangle(normalHit.X, normalHit.Y, normalHit.W, normalHit.H),
                            ARest = normalHit.ARest,
                            BDefend = normalHit.BDefend,
                            Effect = normalHit.Effect,
                            FallPoints = normalHit.Fall,
                            Injury = normalHit.Injury,
                            VRest = normalHit.VRest,
                            ZWidth = normalHit.ZWidth,
                            VelocityDelta = new Vector3(normalHit.DVX, normalHit.DVY, 0)
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
                            Area = new Rectangle(catchDoP.X, catchDoP.Y, catchDoP.W, catchDoP.H),
                            CatchingActBack = catchDoP.CatchingActBack,
                            CatchingActFront = catchDoP.CatchingActFront,
                            CaughtActBack = catchDoP.CaughtActBack,
                            CaughtActFront = catchDoP.CaughtActFront
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
                            Area = new Rectangle(pickWeapon.X, pickWeapon.Y, pickWeapon.W, pickWeapon.H),
                            VRest = pickWeapon.VRest
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
                            Area = new Rectangle(catchBody.X, catchBody.Y, catchBody.W, catchBody.H),
                            CatchingActBack = catchBody.CatchingActBack,
                            CatchingActFront = catchBody.CatchingActFront,
                            CaughtActBack = catchBody.CaughtActBack,
                            CaughtActFront = catchBody.CaughtActFront
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
                            Area = new Rectangle(falling.X, falling.Y, falling.W, falling.H),
                            BDefend = falling.BDefend,
                            FallPoints = falling.Fall,
                            Injury = falling.Injury,
                            VRest = falling.VRest,
                            VelocityDelta = new Vector3(falling.DVX, 0, 0)
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
                            Area = new Rectangle(weaponStrength.X, weaponStrength.Y, weaponStrength.W, weaponStrength.H),
                            BDefend = weaponStrength.BDefend,
                            FallPoints = weaponStrength.Fall,
                            Injury = weaponStrength.Injury,
                            VelocityDelta = new Vector3(weaponStrength.DVX, 0, 0)
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
                            Area = new Rectangle(superPunch.X, superPunch.Y, superPunch.W, superPunch.H),
                            VRest = superPunch.VRest
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
                            Area = new Rectangle(pickWeapon2.X, pickWeapon2.Y, pickWeapon2.W, pickWeapon2.H),
                            VRest = pickWeapon2.VRest
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
                            Area = new Rectangle(healBall.X, healBall.Y, healBall.W, healBall.H),
                            Injury = healBall.Injury,
                            VelocityDelta = new Vector3(healBall.DVX, 0, 0)
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
                            Area = new Rectangle(reflectiveShield.X, reflectiveShield.Y, reflectiveShield.W, reflectiveShield.H),
                            FallPoints = reflectiveShield.Fall,
                            Injury = reflectiveShield.Injury,
                            VRest = reflectiveShield.VRest,
                            VelocityDelta = new Vector3(reflectiveShield.DVX, 0, 0)
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
                            Area = new Rectangle(sonataOfDeath.X, sonataOfDeath.Y, sonataOfDeath.W, sonataOfDeath.H),
                            Injury = sonataOfDeath.Injury,
                            VRest = sonataOfDeath.VRest,
                            ZWidth = sonataOfDeath.ZWidth
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
                            Area = new Rectangle(sonataOfDeath2.X, sonataOfDeath2.Y, sonataOfDeath2.W, sonataOfDeath2.H),
                            Injury = sonataOfDeath2.Injury,
                            VRest = sonataOfDeath2.VRest,
                            ZWidth = sonataOfDeath2.ZWidth
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
                            Area = new Rectangle(solidObject.X, solidObject.Y, solidObject.W, solidObject.H),
                            VRest = solidObject.VRest
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
                            Area = new Rectangle(windWhirlWind.X, windWhirlWind.Y, windWhirlWind.W, windWhirlWind.H),
                            BDefend = windWhirlWind.BDefend,
                            FallPoints = windWhirlWind.Fall,
                            Injury = windWhirlWind.Injury,
                            VRest = windWhirlWind.VRest,
                            ZWidth = windWhirlWind.ZWidth,
                            VelocityDelta = new Vector3(windWhirlWind.DVX, windWhirlWind.DVY, 0)
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
                            Area = new Rectangle(frostWhirlWind.X, frostWhirlWind.Y, frostWhirlWind.W, frostWhirlWind.H),
                            BDefend = frostWhirlWind.BDefend,
                            FallPoints = frostWhirlWind.Fall,
                            Injury = frostWhirlWind.Injury,
                            VRest = frostWhirlWind.VRest,
                            ZWidth = frostWhirlWind.ZWidth,
                            VelocityDelta = new Vector3(frostWhirlWind.DVX, frostWhirlWind.DVY, 0)
                        };
                    }
                    break;
                }
            }
            return convFrameElement;
        }
    }
}