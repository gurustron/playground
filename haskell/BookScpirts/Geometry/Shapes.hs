module Shapes (
    Point(..),
    Shape(..),
    shapeArea,
    nudge,
    baseCircle,
    baseRect
    ) where

data Point = Point Float Float deriving (Show)

data Shape = Circle Point Float | Rectangle Point Point
    deriving (Show)

shapeArea :: Shape -> Float
shapeArea (Circle _ r ) = pi * r^2
shapeArea (Rectangle (Point x1 y1) (Point x2 y2)) = (abs $ x1-x2) * (abs $ y1-y2)

nudge :: Shape -> Float->Float->Shape
nudge (Circle (Point x y) r) a b = Circle (Point (x+a) (y+b)) r
nudge (Rectangle (Point x y) (Point x1 y1)) a b = Rectangle (Point (x+a) (y+b)) (Point (x1+a) (y1+b)) 

baseCircle :: Float -> Shape
baseCircle r = Circle (Point 0 0) r

baseRect :: Float -> Float -> Shape
baseRect width height = Rectangle (Point 0 0) (Point width height)

