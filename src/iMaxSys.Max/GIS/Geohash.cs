﻿//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Geohash.cs
//摘要: Geohash
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-07-04
//----------------------------------------------------------------

using System;

namespace iMaxSys.Max.GIS
{
    public static class Geohash
    {
        public enum Direction
        {
            Top = 0,
            Right = 1,
            Bottom = 2,
            Left = 3
        }

        private const string Base32 = "0123456789bcdefghjkmnpqrstuvwxyz";
        private static readonly int[] Bits = { 16, 8, 4, 2, 1 };

        private static readonly string[][] Neighbors = {
                                                           new[]
                                                               {
                                                                   "p0r21436x8zb9dcf5h7kjnmqesgutwvy", // Top
                                                                   "bc01fg45238967deuvhjyznpkmstqrwx", // Right
                                                                   "14365h7k9dcfesgujnmqp0r2twvyx8zb", // Bottom
                                                                   "238967debc01fg45kmstqrwxuvhjyznp", // Left
                                                               }, new[]
                                                                      {
                                                                          "bc01fg45238967deuvhjyznpkmstqrwx", // Top
                                                                          "p0r21436x8zb9dcf5h7kjnmqesgutwvy", // Right
                                                                          "238967debc01fg45kmstqrwxuvhjyznp", // Bottom
                                                                          "14365h7k9dcfesgujnmqp0r2twvyx8zb", // Left
                                                                      }
                                                       };

        private static readonly string[][] Borders = {
                                                         new[] {"prxz", "bcfguvyz", "028b", "0145hjnp"},
                                                         new[] {"bcfguvyz", "prxz", "0145hjnp", "028b"}
                                                     };
        /// <summary>
        /// 计算相邻
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static String CalculateAdjacent(String hash, Direction direction)
        {
            hash = hash.ToLower();

            char lastChr = hash[hash.Length - 1];
            int type = hash.Length % 2;
            var dir = (int)direction;
            string nHash = hash.Substring(0, hash.Length - 1);

            if (Borders[type][dir].IndexOf(lastChr) != -1)
            {
                nHash = CalculateAdjacent(nHash, (Direction)dir);
            }
            return nHash + Base32[Neighbors[type][dir].IndexOf(lastChr)];
        }
        /// <summary>
        /// 细化间隔
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="cd"></param>
        /// <param name="mask"></param>
        public static void RefineInterval(ref double[] interval, int cd, int mask)
        {
            if ((cd & mask) != 0)
            {
                interval[0] = (interval[0] + interval[1]) / 2;
            }
            else
            {
                interval[1] = (interval[0] + interval[1]) / 2;
            }
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="geohash"></param>
        /// <returns></returns>
        public static double[] Decode(String geohash)
        {
            bool even = true;
            double[] lat = { -90.0, 90.0 };
            double[] lon = { -180.0, 180.0 };

            foreach (char c in geohash)
            {
                int cd = Base32.IndexOf(c);
                for (int j = 0; j < 5; j++)
                {
                    int mask = Bits[j];
                    if (even)
                    {
                        RefineInterval(ref lon, cd, mask);
                    }
                    else
                    {
                        RefineInterval(ref lat, cd, mask);
                    }
                    even = !even;
                }
            }

            return new[] { (lat[0] + lat[1]) / 2, (lon[0] + lon[1]) / 2 };
        }
        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="latitude">纬度</param>
        /// <param name="longitude">经度</param>
        /// <param name="precision">精度</param>
        /// <returns></returns>
        public static String Encode(double latitude, double longitude, int precision = 12)
        {
            bool even = true;
            int bit = 0;
            int ch = 0;
            string geohash = "";

            double[] lat = { -90.0, 90.0 };
            double[] lon = { -180.0, 180.0 };

            if (precision < 1 || precision > 20) precision = 12;

            while (geohash.Length < precision)
            {
                double mid;

                if (even)
                {
                    mid = (lon[0] + lon[1]) / 2;
                    if (longitude > mid)
                    {
                        ch |= Bits[bit];
                        lon[0] = mid;
                    }
                    else
                        lon[1] = mid;
                }
                else
                {
                    mid = (lat[0] + lat[1]) / 2;
                    if (latitude > mid)
                    {
                        ch |= Bits[bit];
                        lat[0] = mid;
                    }
                    else
                        lat[1] = mid;
                }

                even = !even;
                if (bit < 4)
                    bit++;
                else
                {
                    geohash += Base32[ch];
                    bit = 0;
                    ch = 0;
                }
            }
            return geohash;
        }

        /// <summary>
        /// 获取九个格子 顺序 本身 上、下、左、右、 左上、 右上、 左下、右下
        /// </summary>
        /// <param name="geohash"></param>
        /// <returns></returns>
        public static String[] getGeoHashExpand(String geohash)
        {

            try
            {
                String geohashTop = CalculateAdjacent(geohash, Direction.Top);//上

                String geohashBottom = CalculateAdjacent(geohash, Direction.Bottom);//下

                String geohashLeft = CalculateAdjacent(geohash, Direction.Left);//左

                String geohashRight = CalculateAdjacent(geohash, Direction.Right);//右


                String geohashTopLeft = CalculateAdjacent(geohashLeft, Direction.Top);//左上

                String geohashTopRight = CalculateAdjacent(geohashRight, Direction.Top);//右上

                String geohashBottomLeft = CalculateAdjacent(geohashLeft, Direction.Bottom);//左下

                String geohashBottomRight = CalculateAdjacent(geohashRight, Direction.Bottom);//右下

                String[] expand = { geohash, geohashTop, geohashBottom, geohashLeft, geohashRight, geohashTopLeft, geohashTopRight, geohashBottomLeft, geohashBottomRight };
                return expand;
            }
            catch
            {
                return null;
            }
        }
    }

}
