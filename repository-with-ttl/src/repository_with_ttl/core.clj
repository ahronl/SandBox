(ns repository-with-ttl.core
  (:require [clj-time.core :as t]
            [clojure.core.async :as a]))

(def ^:private my-time-state (atom {}))
(def ^:private my-state (atom {}))

(defn- periodic-clean [n]
   (let [ks (->> @my-time-state
                 (filter (fn [[k tm]] (t/before? tm (t/minus (t/now) (t/minutes n)))))
                 (keys)
                 (into #{}))
         rm (fn [s] (->> s
                          (remove (fn [[k v]] 
                                    (contains? ks k)))
                          (into {})))]
     (swap! my-time-state rm)
     (swap! my-state rm)))

(defn- run-periodicly [f];;;make it better
  (a/go-loop []
             (prn f)
             (prn "running .. ")
             (f)
             (Thread/sleep 1000)
             (prn "done")
             (recur)))

(defn init [n]
  (run-periodicly (partial periodic-clean n)))

(defn put-in-state [k v]
  (swap! my-state assoc k v)
  (swap! my-time-state assoc k (t/now)))

(defn get-from-state [k]
  (get @my-state k))
