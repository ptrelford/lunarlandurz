﻿module Font

let A = [ [(0,2); (0,1); (1,0); (2,1); (2,2)]; [(0,1);(2,1)] ]
let B = [ [(0,2); (0,0); (2,0); (0,1); (2,2); (0,2)] ]
let C = [ [(2,2); (0,2); (0,0); (2,0)] ]
let D = [ [(0,2); (0,0); (1,0); (2,1); (2,2); (0,2)] ]
let E = [ [(2,2); (0,2); (0,1); (1,1)]; [(0,1); (0,0); (2,0)] ]
let F = [ [(2,0); (0,0); (0,2)]; [(0,1); (1,1)] ] 
let G = [ [(1,1); (2,1); (2,2); (0,2); (0,0); (2,0)] ]
let H = [ [(0,0); (0,2)]; [(2,2); (2,0)]; [(0,1); (2,1)] ]
let I = [ [(2,0); (0,0)]; [(2,2); (0,2)]; [(1,2); (1,0)] ]
let J = [ [(0,1); (0,2); (2,2); (2,0); (0,0)] ]
let K = [ [(0,0); (0,2)]; [(2,0); (0,1); (2,2)] ]
let L = [ [(2,2); (0,2); (0,0)] ]
let M = [ [(0,2); (0,0); (1,1); (2,0); (2,2)] ]
let N = [ [(0,2); (0,0); (2,2); (2,0)] ]
let O = [ [(2,2); (0,2); (0,0); (2,0); (2,2)] ]
let P = [ [(0,1); (2,1); (2,0); (0,0); (0,2)] ]
let Q = [ [(1,2); (0,2); (0,0); (2,0); (2,1); (1,2)]; [(2,2); (1,1)] ]
let R = [ [(0,2); (0,0); (2,0); (2,1); (0,1); (2,2)] ]
let S = [ [(0,2); (2,2); (2,1); (0,1); (0,0); (2,0)] ]
let T = [ [(0,0); (2,0)]; [(1,0); (1,2)] ]
let U = [ [(0,0); (0,2); (2,2); (2,0)] ]
let V = [ [(0,0); (1,2); (2,0)] ]
let W = [ [(0,0); (0,2); (1,1); (2,2); (2,0)] ]
let X = [ [(0,0); (2,2)]; [(2,0); (0,2)] ]
let Y = [ [(0,0); (1,1); (2,0)]; [(1,1); (1,2)] ]
let Z = [ [(0,0); (2,0); (0,2); (2,2)] ]

let N0 = [ [(0,0); (2,0); (2,2); (0,2); (0,0)]; [(0,2); (2,0)] ]
let N1 = [ [(1,0); (1,2)] ]
let N2 = [ [(0,0); (2,0); (2,1); (0,1); (0,2); (2,2)] ]
let N3 = [ [(0,0); (2,0); (2,2); (0,2)]; [(0,1); (2,1)] ]
let N4 = [ [(0,0); (0,1); (2,1)]; [(2,0); (2,2)] ]
let N5 = [ [(0,2); (2,2); (2,1); (0,1); (0,0); (2,0)] ]
let N6 = [ [(2,0); (0,0); (0,2); (2,2); (2,1); (0,1)] ]
let N7 = [ [(0,0); (2,0); (2,2)] ]
let N8 = [ [(0,0); (2,0); (2,1); (0,1); (0,2); (2,2); (2,1); (0,1); (0,0)] ]
let N9 = [ [(2,2); (2,0); (0,0); (0,1); (2,1)] ] 

let BackSlash = [ [(2,0); (0,2)] ]
let ForwardSlash = [ [(0,2); (2,0)] ]
let Dot = [ [(1,1); (2,1); (2,2); (1,2); (1,1)] ]

let mappings = 
    [ ('A',A); ('B',B); ('C',C); ('D',D); ('E',E); ('F',F); ('G',G); ('H',H); ('I',I); ('J',J); ('K',K); ('L',L); ('M',M);
        ('N',N); ('O',O); ('P',P); ('Q',Q); ('R',R); ('S',S); ('T',T); ('U',U); ('V',V); ('W',W); ('X',X); ('Y',Y); ('Z',Z); 
        ('0',N0); ('1',N1); ('2',N2); ('3',N3); ('4',N4); ('5',N5); ('6',N6); ('7',N7); ('8',N8); ('9',N9);
        ('\\',BackSlash); ('/',ForwardSlash); ('.',Dot) ] 

