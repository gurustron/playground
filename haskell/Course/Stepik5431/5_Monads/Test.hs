-- 5.4 Список и Maybe как монады

pythagoreanTriple :: Int -> [(Int, Int, Int)]
pythagoreanTriple x = do
    c <- [1..x]
    a <- [1..x]
    b <- [a..x]
    if a*a + b*b == c*c then "x" else []
    return (a,b,c)

