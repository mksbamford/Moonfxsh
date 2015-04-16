﻿using Moonfish.Graphics;
using System;
using System.Linq;
using System.Windows.Forms;
using Moonfish.Compiler;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using Moonfish.Guerilla;
using Moonfish.Tags;

namespace Moonfish
{
    static class main
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main( )
        {
            //GuerillaToEnt ripperEnt = new GuerillaToEnt(Local.GuerillaPath);
            //foreach (var tag in Guerilla.Guerilla.h2Tags)
            //{
            //    ripperEnt.DumpTagLayout(tag, Local.PluginsFolder);
            // }
            //Validator v = new Validator();
            //return;

            //Guerilla.Guerilla.LoadGuerillaExecutable(Local.GuerillaPath);
            //var matg = Guerilla.Guerilla.h2Tags.Single( x => x.Class == TagClass.Matg );
            //var test = new MoonfishTagGroup( matg );

            //Test.MakeNewDefinition( );
            var tagClass = TagClass.Scnr;
            GuerillaCs guerilla = new GuerillaCs(Local.GuerillaPath);
            foreach (var tag in Guerilla.Guerilla.h2Tags.Where(x => x.Class ==tagClass))
            {
                guerilla.DumpTagLayout(new MoonfishTagGroup(tag),
                    @"C:\Users\seed\Documents\Visual Studio 2012\Projects\Moonfxsh\Guerilla\Tags");
                Application.DoEvents();
            }
            var files = Directory.GetFiles( Local.MapsDirectory, "*.map", SearchOption.TopDirectoryOnly );

            var validator = new Validator( );
            Guerilla.Guerilla.LoadGuerillaExecutable( Local.GuerillaPath );
            foreach (var tag in Guerilla.Guerilla.h2Tags.Where(x => x.Class == tagClass))
            {
                validator.Validate( new MoonfishTagGroup( tag ),
                    Guerilla.Guerilla.h2Tags.Select( x => new MoonfishTagGroup( x ) ), files );
            }

            return;

            MapStream map = new MapStream(@"C:\Users\seed\Documents\Halo 2 Modding\headlong.map");

            StaticBenchmark.Begin();


    foreach (var tag in map.Where(x => x.Class == TagClass.Bitm))
            {
                const string folder = @"C:\Users\seed\Documents\Visual Studio 2012\Projects\Moonfxsh\Guerilla\Debug\test.bin";
                var fileName = Path.Combine(folder);
                var directoryName = Path.GetDirectoryName(fileName) ?? Path.GetPathRoot(fileName);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                using (
                    var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite, 8 * 1024)
                    )
                {
                    var binaryWriter = new BinaryWriter(fileStream);
                    binaryWriter.Write(map.Deserialize( tag ));
                }

            }
            StaticBenchmark.End();

            Decompiler d = new Decompiler();
            //d.Decompile(new MapStream(@"C:\Users\seed\Documents\Halo 2 Modding\headlong.map"));
            return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new Form1());
        }
    }
}
