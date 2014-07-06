csharp-fast-fibonacci
=====================

Implementation of the Fast Doubling Fibonacci algorithm.

The Fibonacci sequence is defined as F(0)=0, F(1)=1 and F(n)=F(n-1)+F(n-2) for n >= 2;

The Fast doubling method:
Given F(k) and F(k+1), we can calculate:

F(2k) = F(k)[2*F(k+1)-F(k)]
F(2k+1) = F(k+1)^2 + F(k)^2
