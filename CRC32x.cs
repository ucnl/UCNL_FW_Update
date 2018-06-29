using System;

namespace UCNL_FWUpdate
{
    public static class CRC32x
    {
        static uint CRC32_POLY   = 0x04C11DB7;
        static uint CRC32_POLY_R = 0xEDB88320;
        
        static uint[] crc32_table = new uint[256];
        static uint[] crc32r_table = new uint[256];

        private static uint htonl(uint value)
        {
            var bt = BitConverter.GetBytes(value);
            byte[] btr = new byte[bt.Length];

            for (int i = 0; i < bt.Length; i++)
            {
                btr[i] = bt[bt.Length - i - 1];
            }

            return BitConverter.ToUInt32(btr, 0);
        }

        public static void crc32_init()
        {
            uint i, j;
            uint c, cr;
            for (i = 0; i < 256; ++i)
            {
                cr = i;
                c = i << 24;
                for (j = 8; j > 0; --j)
                {
                    c = ((c & 0x80000000) != 0) ? (c << 1) ^ CRC32_POLY : (c << 1);
                    cr = ((cr & 0x00000001) != 0) ? (cr >> 1) ^ CRC32_POLY_R : (cr >> 1);
                }
                crc32_table[i] = c;
                crc32r_table[i] = cr;
            }
        }

        public static uint crc32_stm32(uint init_crc, uint[] buf)
        {
            uint v;
            uint crc;
            crc = ~init_crc;
            uint i = 0;
            int len = buf.Length * 4;

            while (len >= 4)
            {
                v = htonl(buf[i]);
                crc = ( crc << 8 ) ^ crc32_table[0xFF & ( (crc >> 24) ^ (v ) )];
                crc = ( crc << 8 ) ^ crc32_table[0xFF & ( (crc >> 24) ^ (v >> 8) )];
                crc = ( crc << 8 ) ^ crc32_table[0xFF & ( (crc >> 24) ^ (v >> 16) )];
                crc = ( crc << 8 ) ^ crc32_table[0xFF & ( (crc >> 24) ^ (v >> 24) )];
                len -= 4;
                i++;
            }
            
            return ~crc;
        }

        public static uint crc32_stm32(uint init_crc, byte[] buf, int length)
        {
            uint[] ubuf = new uint[length / 4];

            for (int i = 0; i < ubuf.Length; i++)
            {
                ubuf[i] = BitConverter.ToUInt32(buf, i * 4);
            }

            return CRC32x.crc32_stm32(init_crc, ubuf);

        }

    }
}
