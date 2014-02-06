let pattern = [(1,1);(1,2);(2,3);(2,4)]
let neighbours (x, y) =
    [ for i in x-1..x+1 do 
      for j in y-1..y+1 do 
      if not (i = x && j = y) then yield (i,j) ]
