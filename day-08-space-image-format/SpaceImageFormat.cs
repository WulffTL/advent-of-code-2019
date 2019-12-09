using System;
using System.Collections.Generic;

namespace advent_of_code_2019
{
    class SpaceImageFormat
    {
        string inputFilePath;
        int width;
        int height;
        List<int[,]> imageLayers;
        Dictionary<int, Dictionary<int,int>> pixelCountsPerLayer;

        public SpaceImageFormat(string inputFilePath, int width, int height)
        {
            this.inputFilePath = inputFilePath;
            this.width = width;
            this.height = height;
            this.imageLayers = new List<int[,]>();
            this.pixelCountsPerLayer = new Dictionary<int, Dictionary<int, int>>();
        }

        public void PrintAnswer1()
        {
            SetListFromInput();
            for(int i = 0; i < this.imageLayers.Count; i++)
            {
                this.pixelCountsPerLayer.Add(i, GetDigitCounts(this.imageLayers[i]));
            }
            var minZeroCount = int.MaxValue;
            var hightestCountIndex = 0;
            for(int i = 0; i < this.pixelCountsPerLayer.Count; i++)
            {
                var zeroCount = this.pixelCountsPerLayer.GetValueOrDefault(i).GetValueOrDefault(0);
                if(zeroCount < minZeroCount)
                {
                    minZeroCount = zeroCount;
                    hightestCountIndex = i;
                }
            }
            var onesCount = this.pixelCountsPerLayer.GetValueOrDefault(hightestCountIndex).GetValueOrDefault(1);
            var twoCount = this.pixelCountsPerLayer.GetValueOrDefault(hightestCountIndex).GetValueOrDefault(2);
            Console.WriteLine(onesCount * twoCount);
        }

        public void PrintAnswer2()
        {
            SetListFromInput();
            var finalImage = GetImageFromLayers();
            for(int i = 0; i < this.height; i++)
            {
                for(int j = 0; j < this.width; j++)
                {
                    var a = finalImage[i,j] == 1 ? '1' : ' ';
                    Console.Write($"{a} ");
                }
                Console.WriteLine();
            }
        }

        private void SetListFromInput()
        {
            var text = System.IO.File.ReadAllText(this.inputFilePath);
            var encodedLayerLength = this.height * this.width; 
            for(int i = 0; i < text.Length; i += encodedLayerLength)
            {
                this.imageLayers.Add(getLayer(text.Substring(i,encodedLayerLength)));
            }
        }

        private int[,] GetImageFromLayers()
        {
            var finalImage = new int[this.height, this.width];
            for(int i = 0; i < this.height; i++)
            {
                for(int j = 0; j < this.width; j++)
                {
                    foreach (var layer in this.imageLayers)
                    {
                        if(layer[i,j] != 2)
                        {
                            finalImage[i,j] = layer[i,j];
                            break;
                        }
                    }
                }
            }
            return finalImage;
        }

        private Dictionary<int,int> GetDigitCounts(int[,] layer)
        {
            var counts = new Dictionary<int,int>();
            for(int i = 0; i < layer.GetLength(0); i++)
            {
                for(int j = 0; j < layer.GetLength(1); j++)
                {
                    var pixel = layer[i,j];
                    if(counts.GetValueOrDefault(pixel, -1) == -1)
                    {
                        counts.Add(pixel, 1);
                    }
                    else
                    {
                        counts[pixel] = counts[pixel] + 1;
                    }
                }
            }
            return counts;
        }

        private int[,] getLayer(string endcodedLayer)
        {
            var layer = new int[this.height, this.width];
            for(int i = 0; i < this.height; i++)
            {
                for(int j = 0; j < this.width; j++)
                {
                    layer[i,j] = int.Parse(endcodedLayer[i*this.width + j].ToString());
                }
            }
            return layer;
        }
    }
}