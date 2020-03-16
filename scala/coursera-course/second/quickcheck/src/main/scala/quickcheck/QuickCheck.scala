package quickcheck

import org.scalacheck._
import Arbitrary._
import Gen._
import Prop._

abstract class QuickCheckHeap extends Properties("Heap") with IntHeap {

  lazy val genHeap: Gen[H] = oneOf(
    const(empty),
//    const(insert(1,empty)),
//    const(insert(0,insert(2,insert(1,empty)))),
    for {
      a <- arbitrary[Int]
      h <- oneOf(const(empty), genHeap)
    } yield insert(a, h)
  )

  implicit lazy val arbHeap: Arbitrary[H] = Arbitrary(genHeap)

  property("gen1") = forAll { (h: H) =>
    val m = if (isEmpty(h)) 0 else findMin(h)
    findMin(insert(m, h)) == m
  }

  property("meldNil") = forAll { (h: H) =>
    meld(h, empty) == h
  }

  property("Nilmeld") = forAll { (h: H) =>
    meld(empty, h) == h
  }

  property("NewMin") = forAll { (h: H) =>
    if(isEmpty(h))
      true
    else {
      val min = findMin(h)
      val newMin = if (min == Int.MinValue) Int.MinValue else  min - 1
      val x = insert(newMin, h)
      findMin(x) == newMin
    }
  }

  property("addDelete") = forAll{ h: H =>
    deleteMin(insert(Int.MinValue, h)) == h
  }

  property("addDelete2") = forAll{ h: H =>
    if(isEmpty(h))
      true
    else if(findMin(h) == Int.MaxValue)
      true
    else
      deleteMin(insert(Int.MaxValue, h)) != h
  }

  property("min1") = forAll { a: Int =>
    val h = insert(a, empty)
    findMin(h) == a
  }

  property("min2") = forAll { a: Int =>
    val h = insert(a,insert(Int.MaxValue,insert(Int.MinValue, empty)))
    findMin(h) == Int.MinValue
  }

  property("findMin") = forAll{ h: H =>
    findMin(insert(Int.MinValue, h)) == Int.MinValue
  }

  property("deleteMin2") = forAll { (h: H) =>
    if(isEmpty(h))
      true
    else {
      val min = findMin(h)
      val y = deleteMin(h)
      findMin(insert(min, y)) == min
    }
  }
}
