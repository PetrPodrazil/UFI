using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UFITest
{
    [TestClass]
    public class UnitTestUFI
    {
        [TestMethod]
        public void AT()
        {
            Assert.AreEqual("C23S-PQ2V-AMH9-VVRF", UFIGenerator.UFI.GenerateUFI("ATU12345678", 178956970));
        }

        [TestMethod]
        public void BE()
        {
            Assert.AreEqual("U1JV-SUMH-N988-U751", UFIGenerator.UFI.GenerateUFI("BE0987654321", 89478485));
        }

        [TestMethod]
        public void BG()
        {
            Assert.AreEqual("A1JV-EUD3-498W-U23R", UFIGenerator.UFI.GenerateUFI("BG987654321", 89478485));
            Assert.AreEqual("80SW-N2UD-FGRY-F6DA", UFIGenerator.UFI.GenerateUFI("BG9987654321", 252644556));
        }

        [TestMethod]
        public void CZ()
        {
            Assert.AreEqual("F000-T0KH-V002-T4SM", UFIGenerator.UFI.GenerateUFI("CZ978563421", 0));
            Assert.AreEqual("HKC1-Q7N0-DKF6-7N3E", UFIGenerator.UFI.GenerateUFI("CZ81726354", 156920229));
            Assert.AreEqual("24VQ-VGV0-WF16-7WC9", UFIGenerator.UFI.GenerateUFI("CZ978563421", 15790899));
            Assert.AreEqual("W3NN-SKC4-JXSS-V4WG", UFIGenerator.UFI.GenerateUFI("CZ9785634210", 268435455));
        }

        [TestMethod]
        public void CY()
        {
            Assert.AreEqual("7EHW-3KC7-748V-S1RH", UFIGenerator.UFI.GenerateUFI("CY12345678C", 87654321));
            Assert.AreEqual("FEHW-3KH5-X487-SNRD", UFIGenerator.UFI.GenerateUFI("CY12345678Y", 87654321));
        }

        [TestMethod]
        public void DE()
        {
            Assert.AreEqual("KMTT-DSP3-7FD7-6RWY", UFIGenerator.UFI.GenerateUFI("DE112358132", 134217728));
        }

        [TestMethod]
        public void DK()
        {
            Assert.AreEqual("3FQU-5GP0-Y105-J64N", UFIGenerator.UFI.GenerateUFI("DK31415926", 524544));
        }

        [TestMethod]
        public void EE()
        {
            Assert.AreEqual("QY3Q-327C-QDPR-EE11", UFIGenerator.UFI.GenerateUFI("EE271828182", 230087533));
        }

        [TestMethod]
        public void ES()
        {
            Assert.AreEqual("EF00-E0R3-M00S-4CX8", UFIGenerator.UFI.GenerateUFI("ESA1234567B", 5));
            Assert.AreEqual("6XC1-Y0C0-N004-JXWK", UFIGenerator.UFI.GenerateUFI("ES81234567C", 500));
            Assert.AreEqual("Q9UE-K0GC-V00M-8ND0", UFIGenerator.UFI.GenerateUFI("ESD12345678", 5000));
        }


        [TestMethod]
        public void FR()
        {
            Assert.AreEqual("6KTT-PSK1-AFDM-KEFU", UFIGenerator.UFI.GenerateUFI("FRRF987654321", 134217728));
            Assert.AreEqual("NX3Q-2263-2DP5-UQ4T", UFIGenerator.UFI.GenerateUFI("FRZY999999999", 230087533));
            Assert.AreEqual("F3NN-3K1J-EXSK-7PHY", UFIGenerator.UFI.GenerateUFI("FR01012345678", 268435455));
            Assert.AreEqual("23NN-5KKE-GXSF-7MD3", UFIGenerator.UFI.GenerateUFI("FR10012345678", 268435455));
        }

        [TestMethod]
        public void GB()
        {
            Assert.AreEqual("GJC1-S7TH-AKFK-TR8P", UFIGenerator.UFI.GenerateUFI("GB987654321", 156920229));
            Assert.AreEqual("CJC1-47ES-AKFS-WTFW", UFIGenerator.UFI.GenerateUFI("GB999987654321", 156920229));
            Assert.AreEqual("3JC1-47ET-6KFH-WMV8", UFIGenerator.UFI.GenerateUFI("GB999999999999", 156920229));
            Assert.AreEqual("53NN-7KTT-1XS1-DDPH", UFIGenerator.UFI.GenerateUFI("GBZY123", 268435455));
            Assert.AreEqual("M3NN-7KTS-YXSK-D8UW", UFIGenerator.UFI.GenerateUFI("GBAB987", 268435455));
        }

        [TestMethod]
        public void GR()
        {
            Assert.AreEqual("QNWM-9X6E-E46N-G4GJ", UFIGenerator.UFI.GenerateUFI("GR567438921", 66260700));
        }

        [TestMethod]
        public void FI()
        {
            Assert.AreEqual("VWF9-CDT4-2S2N-PDTV", UFIGenerator.UFI.GenerateUFI("FI18273645", 29979245));
        }

        [TestMethod]
        public void HR()
        {
            Assert.AreEqual("53NN-KKPX-SXSD-QJY7", UFIGenerator.UFI.GenerateUFI("HR16021765654", 268435455));
        }

        [TestMethod]
        public void HU()
        {
            Assert.AreEqual("AU06-7HHD-64QN-8RHF", UFIGenerator.UFI.GenerateUFI("HU22334455", 238219293));
        }

        [TestMethod]
        public void IE()
        {
            Assert.AreEqual("GMTT-2SQN-6FDD-6TV1", UFIGenerator.UFI.GenerateUFI("IE9Z54321Y", 134217728));
            Assert.AreEqual("KMTT-2SQQ-0FDQ-6A5D", UFIGenerator.UFI.GenerateUFI("IE9+54321Y", 134217728));
            Assert.AreEqual("GMTT-2SQR-UFD0-6TFR", UFIGenerator.UFI.GenerateUFI("IE9*54321Y", 134217728));
            Assert.AreEqual("JY3Q-R2M8-GDP2-DQRS", UFIGenerator.UFI.GenerateUFI("IE9876543Z", 230087533));
            Assert.AreEqual("XY3Q-S215-2DPF-DA4U", UFIGenerator.UFI.GenerateUFI("IE9876543ZW", 230087533));
            Assert.AreEqual("TUG4-PE6C-4XHP-RSAM", UFIGenerator.UFI.GenerateUFI("IE9876543AB", 182319099));
        }


        [TestMethod]
        public void IS()
        {
            Assert.AreEqual("XUG4-WE32-RXHD-RHWU", UFIGenerator.UFI.GenerateUFI("ISAB3D5F", 182319099));
            Assert.AreEqual("53NN-1KDC-JXSH-W6WV", UFIGenerator.UFI.GenerateUFI("IS1ZY2BA", 268435455));
        }

        [TestMethod]
        public void IT()
        {
            Assert.AreEqual("WK3F-PYSX-TXM0-K9AV", UFIGenerator.UFI.GenerateUFI("IT14286244833", 214783315));
        }

        [TestMethod]
        public void LI()
        {
            Assert.AreEqual("CUG4-FEHP-HXHW-SC4E", UFIGenerator.UFI.GenerateUFI("LI99999", 182319099));
        }

        [TestMethod]
        public void LT()
        {
            Assert.AreEqual("W3VQ-HGW8-UF12-W7QP", UFIGenerator.UFI.GenerateUFI("LT987654321", 15790899));
            Assert.AreEqual("SJC1-P7FR-DKF3-Y2YC", UFIGenerator.UFI.GenerateUFI("LT987654321098", 156920229));
        }

        [TestMethod]
        public void LU()
        {
            Assert.AreEqual("FK3F-8YC6-1XMK-SAQ5", UFIGenerator.UFI.GenerateUFI("LU16726218", 214783315));
        }


        [TestMethod]
        public void LV()
        {
            Assert.AreEqual("WUG4-5E2S-UXHN-M5C7", UFIGenerator.UFI.GenerateUFI("LV39903127176", 182319099));
        }


        [TestMethod]
        public void MT()
        {
            Assert.AreEqual("7KW0-SMM5-2Q7N-4K8D", UFIGenerator.UFI.GenerateUFI("MT99887766", 83144621));
        }

        [TestMethod]
        public void NL()
        {
            Assert.AreEqual("QJ0V-J1JU-9Y8W-37TG", UFIGenerator.UFI.GenerateUFI("NL999999999B77", 96485337));
        }

        [TestMethod]
        public void NO()
        {
            Assert.AreEqual("63NN-7KPT-WXS8-WGYA", UFIGenerator.UFI.GenerateUFI("NO958473621", 268435455));
        }


        [TestMethod]
        public void PL()
        {
            Assert.AreEqual("XRMW-9HU2-PT1U-7JNN", UFIGenerator.UFI.GenerateUFI("PL4835978701", 19621109));
        }


        [TestMethod]
        public void PT()
        {
            Assert.AreEqual("K9WS-JKK3-WS2E-1WSC", UFIGenerator.UFI.GenerateUFI("PT998776554", 30051977));
        }

        [TestMethod]
        public void RO()
        {
            Assert.AreEqual("E0SW-U2CF-KGR6-FRKW", UFIGenerator.UFI.GenerateUFI("RO98", 252644556));
            Assert.AreEqual("2K3F-QYHK-YXMU-RTN5", UFIGenerator.UFI.GenerateUFI("RO9081726354", 214783315));
        }


        [TestMethod]
        public void SE()
        {
            Assert.AreEqual("1KC1-87DH-0KFR-2WGE", UFIGenerator.UFI.GenerateUFI("SE987654321098", 156920229));
        }

        [TestMethod]
        public void SI()
        {
            Assert.AreEqual("U23S-WQK5-AMH7-V03N", UFIGenerator.UFI.GenerateUFI("SI12345678", 178956970));
        }

        [TestMethod]
        public void SK()
        {
            Assert.AreEqual("N0SW-W2AP-FGRV-F9RH", UFIGenerator.UFI.GenerateUFI("SK9987654321", 252644556));
        }

        [TestMethod]
        public void NoVATIN()
        {
            Assert.AreEqual("NJC1-671A-UKF3-J0M8", UFIGenerator.UFI.GenerateUFI("1828639338661", 156920229));
        }
    }
}
