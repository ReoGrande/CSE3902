

<<<<<<< HEAD
// using System;
// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;
// using System.Collections;
// using System.ComponentModel;
// using System.Reflection.Metadata;
// using Microsoft.Xna.Framework.Input;
// using System.Runtime.InteropServices;
// using static System.Formats.Asn1.AsnWriter;
// using static sprint0.Link;


// namespace sprint0
// {
//     public interface ISound
//     {
=======
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.ComponentModel;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.Audio;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Link;




namespace sprint0
{
    public interface ISound
    {
>>>>>>> 7213db1bef3469b4e422da600b073ee899a1d395


        void Play();

        void Play(float volume);



//     }



<<<<<<< HEAD

//     public class Sound : ISound
//     {
=======
    public class Sound : ISound
    {
>>>>>>> 7213db1bef3469b4e422da600b073ee899a1d395

//         private SoundEffect soundEffect;
//         public Sound(SoundEffect soundEffect)
//         {
//             this.soundEffect = soundEffect;
//         }

//         public void Play()
//         {

<<<<<<< HEAD
//             soundEffect.play();

//         }
=======
            soundEffect.Play();

        }
        public void Play(float volume)
        {

            soundEffect.Play(volume, 0, 0);
>>>>>>> 7213db1bef3469b4e422da600b073ee899a1d395

        }

//     }







// }