using System.Linq;
using LF2datConverter.dat.Convenient;
using Microsoft.Xna.Framework;
using BloodPoint = LF2datConverter.dat.Base.BloodPoint;
using Body = LF2datConverter.dat.Base.Body;
using CatchBody = LF2datConverter.dat.Base.CatchBody;
using CatchDoP = LF2datConverter.dat.Base.CatchDoP;
using CatchingPoint = LF2datConverter.dat.Base.CatchingPoint;
using CaughtPoint = LF2datConverter.dat.Base.CaughtPoint;
using Falling = LF2datConverter.dat.Base.Falling;
using FrostWhirlWind = LF2datConverter.dat.Base.FrostWhirlWind;
using HealBall = LF2datConverter.dat.Base.HealBall;
using NormalHit = LF2datConverter.dat.Base.NormalHit;
using ObjectPoint = LF2datConverter.dat.Base.ObjectPoint;
using PickWeapon = LF2datConverter.dat.Base.PickWeapon;
using PickWeapon2 = LF2datConverter.dat.Base.PickWeapon2;
using ReflectiveShield = LF2datConverter.dat.Base.ReflectiveShield;
using SolidObject = LF2datConverter.dat.Base.SolidObject;
using SonataOfDeath = LF2datConverter.dat.Base.SonataOfDeath;
using SonataOfDeath2 = LF2datConverter.dat.Base.SonataOfDeath2;
using SuperPunch = LF2datConverter.dat.Base.SuperPunch;
using WeaponPoint = LF2datConverter.dat.Base.WeaponPoint;
using WeaponStrength = LF2datConverter.dat.Base.WeaponStrength;
using WindWhirlWind = LF2datConverter.dat.Base.WindWhirlWind;

namespace Lf2datConverter.dat
{
    static class ConvenientConverter
    {
        public static Character ConvertToConvenientCharacter(LF2datConverter.dat.Base.Character character)
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

        private static SpriteFile ConvertSpriteFile(LF2datConverter.dat.Base.SpriteFile file)
        {
            var convFile = new SpriteFile
            {
                PicturesInColumn = file.Col,
                FinishID = file.FinishID,
                Height = file.H,
                PicturesInRow = file.Row,
                Sprite = file.Sprite,
                StartID = file.StartID,
                Width = file.W
            };
            return convFile;
        }

        private static CharacterFrame ConvertCharacterFrame(LF2datConverter.dat.Base.CharacterFrame frame)
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

        private static FrameElement ConvertFrameElement(LF2datConverter.dat.Base.FrameElement frameElement)
        {
            FrameElement convFrameElement = null;
            switch (frameElement.Name)
            {
                case "WeaponPoint":
                {
                    var weaponPoint = frameElement as WeaponPoint;
                    if (weaponPoint != null)
                        convFrameElement = new LF2datConverter.dat.Convenient.WeaponPoint
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
                    var objectPoint = frameElement as ObjectPoint;
                    if (objectPoint != null)
                    {
                        convFrameElement = new LF2datConverter.dat.Convenient.ObjectPoint
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
                    var catchingPoint = frameElement as CatchingPoint;
                    if (catchingPoint != null)
                    {
                        convFrameElement = new LF2datConverter.dat.Convenient.CatchingPoint
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
                    var caughtPoint = frameElement as CaughtPoint;
                    if (caughtPoint != null)
                    {
                        convFrameElement = new LF2datConverter.dat.Convenient.CaughtPoint
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
                    var bloodPoint = frameElement as BloodPoint;
                    if (bloodPoint != null)
                    {
                        convFrameElement = new LF2datConverter.dat.Convenient.BloodPoint
                        {
                            Position = new Point(bloodPoint.X, bloodPoint.Y)
                        };
                    }
                    break;
                }
                    case "Body":
                {
                    var body = frameElement as Body;
                    if (body != null)
                    {
                        convFrameElement = new LF2datConverter.dat.Convenient.Body
                        {
                            Area = new Rectangle(body.X, body.Y, body.W, body.H),
                            Kind = body.Kind
                        };
                    }
                    break;
                }
                    case "NormalHit":
                {
                    var normalHit = frameElement as NormalHit;
                    if (normalHit != null)
                    {
                        convFrameElement = new LF2datConverter.dat.Convenient.NormalHit
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
                    var catchDoP = frameElement as CatchDoP;
                    if (catchDoP != null)
                    {
                        convFrameElement = new LF2datConverter.dat.Convenient.CatchDoP
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
                    var pickWeapon = frameElement as PickWeapon;
                    if (pickWeapon != null)
                    {
                        convFrameElement = new LF2datConverter.dat.Convenient.PickWeapon
                        {
                            Area = new Rectangle(pickWeapon.X, pickWeapon.Y, pickWeapon.W, pickWeapon.H),
                            VRest = pickWeapon.VRest
                        };
                    }
                    break;
                }
                    case "CatchBody":
                {
                    var catchBody = frameElement as CatchBody;
                    if (catchBody != null)
                    {
                        convFrameElement = new LF2datConverter.dat.Convenient.CatchBody
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
                    var falling = frameElement as Falling;
                    if (falling != null)
                    {
                        convFrameElement = new LF2datConverter.dat.Convenient.Falling
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
                    var weaponStrength = frameElement as WeaponStrength;
                    if (weaponStrength != null)
                    {
                        convFrameElement = new LF2datConverter.dat.Convenient.WeaponStrength
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
                    var superPunch = frameElement as SuperPunch;
                    if (superPunch != null)
                    {
                        convFrameElement = new LF2datConverter.dat.Convenient.SuperPunch
                        {
                            Area = new Rectangle(superPunch.X, superPunch.Y, superPunch.W, superPunch.H),
                            VRest = superPunch.VRest
                        };
                    }
                    break;
                }
                    case "PickWeapon2":
                {
                    var pickWeapon2 = frameElement as PickWeapon2;
                    if (pickWeapon2 != null)
                    {
                        convFrameElement = new LF2datConverter.dat.Convenient.PickWeapon2
                        {
                            Area = new Rectangle(pickWeapon2.X, pickWeapon2.Y, pickWeapon2.W, pickWeapon2.H),
                            VRest = pickWeapon2.VRest
                        };
                    }
                    break;
                }
                    case "HealBall":
                {
                    var healBall = frameElement as HealBall;
                    if (healBall != null)
                    {
                        convFrameElement = new LF2datConverter.dat.Convenient.HealBall
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
                    var reflectiveShield = frameElement as ReflectiveShield;
                    if (reflectiveShield != null)
                    {
                        convFrameElement = new LF2datConverter.dat.Convenient.ReflectiveShield
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
                    var sonataOfDeath = frameElement as SonataOfDeath;
                    if (sonataOfDeath != null)
                    {
                        convFrameElement = new LF2datConverter.dat.Convenient.SonataOfDeath
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
                    var sonataOfDeath2 = frameElement as SonataOfDeath2;
                    if (sonataOfDeath2 != null)
                    {
                        convFrameElement = new LF2datConverter.dat.Convenient.SonataOfDeath2
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
                    var solidObject = frameElement as SolidObject;
                    if (solidObject != null)
                    {
                        convFrameElement = new LF2datConverter.dat.Convenient.SolidObject
                        {
                            Area = new Rectangle(solidObject.X, solidObject.Y, solidObject.W, solidObject.H),
                            VRest = solidObject.VRest
                        };
                    }
                    break;
                }
                    case "WindWhirlWind":
                {
                    var windWhirlWind = frameElement as WindWhirlWind;
                    if (windWhirlWind != null)
                    {
                        convFrameElement = new LF2datConverter.dat.Convenient.WindWhirlWind
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
                    var frostWhirlWind = frameElement as FrostWhirlWind;
                    if (frostWhirlWind != null)
                    {
                        convFrameElement = new LF2datConverter.dat.Convenient.FrostWhirlWind
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