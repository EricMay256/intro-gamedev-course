// Generated using Copilot
// This code implements a 2D simplex noise generator in C#.
using System;

public class GeneratedSimplexNoise
{
    // Gradients for 2D simplex — 12 directions
    private static readonly int[][] grad3 = {
        new[] {1,1}, new[] {-1,1}, new[] {1,-1}, new[] {-1,-1},
        new[] {1,0}, new[] {-1,0}, new[] {1,0}, new[] {-1,0},
        new[] {0,1}, new[] {0,-1}, new[] {0,1}, new[] {0,-1}
    };

    private readonly short[] perm; // Permutation table for gradient lookup

    public GeneratedSimplexNoise(int? seed = null)
    {
        perm = new short[512];
        var p = new short[256];
        Random random = seed.HasValue ? new Random(seed.Value) : new Random();

        // Fill array with values from 0 to 255
        for (int i = 0; i < 256; i++) p[i] = (short)i;

        // Shuffle using Fisher–Yates
        for (int i = 255; i >= 0; i--)
        {
            int j = random.Next(i + 1);
            (p[i], p[j]) = (p[j], p[i]);
        }

        // Duplicate into perm table for easy overflow
        for (int i = 0; i < 512; i++) perm[i] = p[i & 255];
    }

    // Dot product of gradient and offset vector
    private static double Dot(int[] g, double x, double y) => g[0] * x + g[1] * y;

    // 2D simplex noise generation
    public double Generate(double xin, double yin)
    {
        const double F2 = 0.366025403;            // Skewing factor
        const double G2 = 0.211324865f;       // Unskewing factor 

        // Skew input space to determine which simplex cell we're in
        double s = (xin + yin) * F2;
        int i = (int)Math.Floor(xin + s);
        int j = (int)Math.Floor(yin + s);

        double t = (i + j) * G2;
        double X0 = i - t;
        double Y0 = j - t;
        double x0 = xin - X0;  // Distances from cell origin
        double y0 = yin - Y0;

        // Determine which simplex triangle we're in
        int i1, j1;
        if (x0 > y0) { i1 = 1; j1 = 0; } // lower triangle
        else         { i1 = 0; j1 = 1; } // upper triangle

        // Offsets for remaining corners
        double x1 = x0 - i1 + G2;
        double y1 = y0 - j1 + G2;
        double x2 = x0 - 1.0 + 2.0 * G2;
        double y2 = y0 - 1.0 + 2.0 * G2;

        // Hash coordinates to get gradient indices
        int ii = i & 255;
        int jj = j & 255;

        int gi0 = perm[ii + perm[jj]] % 12;
        int gi1 = perm[ii + i1 + perm[jj + j1]] % 12;
        int gi2 = perm[ii + 1 + perm[jj + 1]] % 12;

        double n0 = 0, n1 = 0, n2 = 0; // Noise contributions

        // Corner 0 contribution
        double t0 = 0.5 - x0 * x0 - y0 * y0;
        if (t0 >= 0)
        {
            t0 *= t0;
            n0 = t0 * t0 * Dot(grad3[gi0], x0, y0);
        }

        // Corner 1 contribution
        double t1 = 0.5 - x1 * x1 - y1 * y1;
        if (t1 >= 0)
        {
            t1 *= t1;
            n1 = t1 * t1 * Dot(grad3[gi1], x1, y1);
        }

        // Corner 2 contribution
        double t2 = 0.5 - x2 * x2 - y2 * y2;
        if (t2 >= 0)
        {
            t2 *= t2;
            n2 = t2 * t2 * Dot(grad3[gi2], x2, y2);
        }

        // Add contributions and scale result to approximate [-1, 1]
        return 70.0 * (n0 + n1 + n2);
    }
}