(ns most-resent-repository.core-test
  (:require [clojure.test :refer :all]
            [most-resent-repository.core :refer :all]))

(deftest most-resent-repository
  (testing "when put-state updates internal state"
    (put-state :a :b)
    (is (= (get-state :a) :b))
    (put-state :a :c)
    (is (= (get-state :a) :c)))
  (testing "when put-state 11 times should override last used key"
    (doseq [i (range 1 11)]
      (let [k (keyword (str i))
            v (str i)]
        (put-state k v)))
    (is (not (nil? (get-state :1))))
    (is (not (nil? (get-state :2))))
    (put-state :a :b)
    (put-state :b :c)
    (is (nil? (get-state :3)))
    (is (nil? (get-state :4)))
    ))
